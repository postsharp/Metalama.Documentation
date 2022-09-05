// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.AWS.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BuildMetalamaDocumentation
{
    internal class DocumentationPublisher : S3Publisher
    {
        public DocumentationPublisher( IReadOnlyCollection<S3PublisherConfiguration> configurations )
            : base( configurations )
        {
        }

        public override SuccessCode PublishFile( BuildContext context, PublishSettings settings, string file, BuildInfo buildInfo, BuildConfigurationInfo configuration )
        {
            var hasEnvironmentError = false;

            if ( string.IsNullOrEmpty( Environment.GetEnvironmentVariable( "DOC_API_KEY" ) ) )
            {
                context.Console.WriteError( $"The DOC_API_KEY environment variable is not defined." );
                hasEnvironmentError = true;
            }

            if ( hasEnvironmentError )
            {
                return SuccessCode.Fatal;
            }

            var successCode = base.PublishFile( context, settings, file, buildInfo, configuration );

            if ( successCode != SuccessCode.Success )
            {
                return successCode;
            }

            const string documentationUrl = "https://postsharp-helpbrowser.azurewebsites.net/";

            context.Console.WriteImportantMessage( $"Invalidating {documentationUrl}" );

            var docApiKey = Environment.GetEnvironmentVariable( "DOC_API_KEY" );

            var httpClient = new HttpClient();
            var invalidationResponse = httpClient.GetAsync( $"{documentationUrl}_api/invalidate?{docApiKey}" ).GetAwaiter().GetResult();

            if ( invalidationResponse.StatusCode != System.Net.HttpStatusCode.OK )
            {
                context.Console.WriteError( "Failed to invalidate documentation cache." );

                return SuccessCode.Fatal;
            }

            return SuccessCode.Success;
        }
    }
}
