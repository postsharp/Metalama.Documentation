﻿using System;
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
            var transformedPath = Path.ChangeExtension(targetPath, ".t.cs");
            var aspectPath = Path.ChangeExtension(targetPath, ".Aspect.cs");

            // Find the directories.
            var projectDir = FindParentDirectory(Path.GetDirectoryName(targetPath),
                directory => Directory.GetFiles(directory, "*.csproj").Length > 0);
            var gitDirectory = FindParentDirectory(Path.GetDirectoryName(targetPath),
                directory => Directory.Exists(Path.Combine(directory, ".git")));

            var targetPathRelativeToProjectDir = GetRelativePath(projectDir, targetPath);
            var sourceDirectoryRelativeToGitDir = GetRelativePath(gitDirectory, Path.GetDirectoryName(targetPath));

            var aspectHtmlPath = Path.GetFullPath(Path.Combine(projectDir, "obj", "html",
                Path.ChangeExtension(targetPathRelativeToProjectDir, ".Aspect.cs.html")));
            var targetHtmlPath = Path.GetFullPath(Path.Combine(projectDir, "obj", "html",
                Path.ChangeExtension(targetPathRelativeToProjectDir, ".cs.html")));
            var transformedHtmlPath = Path.GetFullPath(Path.Combine(projectDir, "obj", "html",
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

                var template = @"
<div class=""sample-links tabbed""><a class=""github"" href=""GIT_URL"">See on GitHub</a> | <a class=""try"" href=""TRY_URL"">Try Online</a></div>
<div class=""tabGroup"">
    <ul>
        <li>
            <a href=""#tabpanel_IDENTIFIER_aspect"">Aspect Code</a>
        </li>
        <li>
            <a href=""#tabpanel_IDENTIFIER_target"">Target Code</a>
        </li>
        <li>
            <a href=""#tabpanel_IDENTIFIER_transformed"">Transformed Code</a>
        </li>
    </ul>
    <div id=""tabpanel_IDENTIFIER_aspect"">
        ASPECT_CODE
    </div>
    <div id=""tabpanel_IDENTIFIER_target"">
        TARGET_CODE
    </div>
    <div id=""tabpanel_IDENTIFIER_transformed"">
        TRANSFORMED_CODE
    </div>
</div>
";

                return template
                    .Replace("IDENTIFIER", Interlocked.Increment(ref nextId).ToString())
                    .Replace("ASPECT_CODE", aspectHtml)
                    .Replace("TARGET_CODE", targetHtml)
                    .Replace("TRANSFORMED_CODE", transformedHtml)
                    .Replace("GIT_URL", gitUrl)
                    .Replace("TRY_URL", tryUrl);
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