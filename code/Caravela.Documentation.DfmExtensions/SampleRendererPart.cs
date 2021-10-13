using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using Microsoft.DocAsCode.Common;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using Newtonsoft.Json;
using PKT.LZStringCSharp;

namespace Caravela.Documentation.DfmExtensions
{
    public class SampleRendererPart : DfmCustomizedRendererPartBase<IMarkdownRenderer, DfmIncludeBlockToken,
        MarkdownBlockContext>
    {
        private static int nextId = 1;

        public override string Name => nameof(SampleRendererPart);

        static SampleRendererPart()
        {
            //System.Diagnostics.Debugger.Launch();
        }


        public override bool Match(IMarkdownRenderer renderer, DfmIncludeBlockToken token, MarkdownBlockContext context)
        {
            return TryParseToken(token, out _);
        }

        private static string HtmlEncode(string s)
        {
            var stringBuilder = new StringBuilder(s.Length);

            foreach (var c in s)
                switch (c)
                {
                    case '<':
                        stringBuilder.Append("&lt;");

                        break;

                    case '>':
                        stringBuilder.Append("&gt;");

                        break;

                    case '&':
                        stringBuilder.Append("&amp;");

                        break;

                    default:
                        stringBuilder.Append(c);

                        break;
                }

            return stringBuilder.ToString();
        }

        private static bool TryParseToken(DfmIncludeBlockToken token,
            out ( string FilePath, string Fragment, NameValueCollection Query ) parsed)
        {
            var targetFileName = UriUtility.GetPath(token.Src);
            if (!targetFileName.EndsWith(".cs"))
            {
                parsed = default;
                return false;
            }


            var parameters = new NameValueCollection();

            if (UriUtility.HasQueryString(token.Src))
                foreach (var part in UriUtility.GetQueryString(token.Src).Split('&'))
                {
                    var nameValuePair = part.Split('=');

                    parameters.Add(nameValuePair[0], nameValuePair.Length > 1 ? nameValuePair[1] : null);
                }

            parsed = (targetFileName, UriUtility.GetFragment(token.Src), parameters);

            return true;
        }

        private static string FindParentDirectory(string directory, Predicate<string> predicate)
        {
            if (directory == null) return null;

            if (predicate(directory))
            {
                return directory;
            }
            else
            {
                var parentDirectory = Path.GetDirectoryName(directory);

                return FindParentDirectory(parentDirectory, predicate);
            }
        }

        public override StringBuffer Render(IMarkdownRenderer renderer, DfmIncludeBlockToken token,
            MarkdownBlockContext context)
        {
            TryParseToken(token, out var source);

            var referencingFile =
                Path.GetFullPath(Path.Combine((string) context.Variables["BaseFolder"], token.SourceInfo.File));

            var shortFileNameWithoutExtension = Path.GetFileNameWithoutExtension(source.FilePath);
            var targetPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(referencingFile), source.FilePath));
            var programOutputPath = Path.ChangeExtension(targetPath, ".t.txt");
            var aspectPath = Path.ChangeExtension(targetPath, ".Aspect.cs");

            // Find the directories.
            var projectDir = FindParentDirectory(Path.GetDirectoryName(targetPath),
                directory => Directory.GetFiles(directory, "*.csproj").Length > 0);
            var gitDirectory = FindParentDirectory(Path.GetDirectoryName(targetPath),
                directory => Directory.Exists(Path.Combine(directory, ".git")));

            var targetPathRelativeToProjectDir = GetRelativePath(projectDir, targetPath);
            var sourceDirectoryRelativeToGitDir = GetRelativePath(gitDirectory, Path.GetDirectoryName(targetPath));

            var aspectHtmlPath = Path.GetFullPath(Path.Combine(projectDir, "obj", "html", "net5.0",
                Path.ChangeExtension(targetPathRelativeToProjectDir, ".Aspect.cs.html")));
            var targetHtmlPath = Path.GetFullPath(Path.Combine(projectDir, "obj", "html", "net5.0",
                Path.ChangeExtension(targetPathRelativeToProjectDir, ".cs.html")));
            var transformedHtmlPath = Path.GetFullPath(Path.Combine(projectDir, "obj", "html", "net5.0",
                Path.ChangeExtension(targetPathRelativeToProjectDir, ".out.cs.html")));
            

            const string gitBranch = "release/0.3";
            const string gitHubProjectPath = "https://github.com/postsharp/Caravela/blob/" + gitBranch;
            const string tryBaseUrl = "https://try.postsharp.net/#";

            if (File.Exists(aspectPath))
            {
                // Create the tab group with the aspect, target, and transformed code.

                var targetHtml = File.ReadAllText(targetHtmlPath);
                var aspectHtml = File.ReadAllText(aspectHtmlPath);
                var transformedHtml = File.ReadAllText(transformedHtmlPath);

                string Html2Text(string html)
                {
                    HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    return HtmlEntity.DeEntitize(doc.DocumentNode.SelectSingleNode("//pre").InnerText);
                }

                var gitUrl = gitHubProjectPath + "/" + sourceDirectoryRelativeToGitDir + "/" +
                             shortFileNameWithoutExtension + ".Aspect.cs";

                var targetCs = Html2Text(targetHtml);
                var aspectCs = Html2Text(aspectHtml);
                var tryPayloadJson = JsonConvert.SerializeObject(new { a = aspectCs, p = targetCs });
                var tryPayloadHash = LZString.CompressToEncodedURIComponent(tryPayloadJson);
                var tryUrl = tryBaseUrl + tryPayloadHash;

                var snippetId = Interlocked.Increment(ref nextId).ToString();
                
                
                var tabHeaders = new StringBuilder();
                var tabBodies = new StringBuilder();

                void AppendTab(string tabId, string header, string content)
                {
                    tabHeaders.Append($"<li><a href=\"#tabpanel_{snippetId}_{tabId}\">{header}</a></li>");
                    tabBodies.Append($"<div id=\"tabpanel_{snippetId}_{tabId}\">{content}</div>");
                }
                
                AppendTab("aspect", "Aspect Code", aspectHtml);
                AppendTab("target", "Target Code", targetHtml);
                AppendTab("transformed", "Transformed Code", transformedHtml);

                if (File.Exists(programOutputPath))
                {
                    AppendTab("output", "Program Output", "<pre class=\"program-output\">" + File.ReadAllText(programOutputPath) + "</pre>" );
                }
                
                var totalContent =  $"<div class=\"sample-links tabbed\"><a class=\"github\" href=\"{gitUrl}\">See on GitHub</a> | <a class=\"try\" href=\"{tryUrl}\">Try Online</a></div><div class=\"tabGroup\"><ul>"
                    + tabHeaders
                    + "</ul>"
                    + tabBodies
                    + "</div>";

                return totalContent;

            }
            else
            {
                var gitUrl = gitHubProjectPath + "/" + sourceDirectoryRelativeToGitDir + "/" +
                             shortFileNameWithoutExtension + ".cs";

                var gitHubLink = @"<div class=""sample-links""><a class=""github"" href=""GIT_URL"">See on GitHub</a></div>"
                    .Replace("GIT_URL", gitUrl);

                if (File.Exists(targetHtmlPath))
                {
                    // Write the syntax-highlighted HTML instead.

                    var html = File.ReadAllText(targetHtmlPath);
                    return gitHubLink + html;
                }
                else
                {
                    return gitHubLink +
                           @"<pre><code class=""lang-csharp"" name=""NAME"">TARGET_CODE</code></pre>"
                               .Replace("TARGET_CODE", File.ReadAllText(targetPath))
                               .Replace("GIT_URL", gitUrl)
                               .Replace("NAME", token.Name);
                }
            }
        }


        private static string GetRelativePath(string projectDir, string targetPath)
        {
            return new Uri(Path.Combine(projectDir, "_")).MakeRelativeUri(new Uri(targetPath)).ToString();
        }
    }
}