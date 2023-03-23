using Metalama.Extensions.Architecture.Fabrics;
using Metalama.Framework.Fabrics;
using System.IO;

namespace Doc.Architecture.NamingConvention_Fabric
{
    internal class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Verify().SelectTypesDerivedFrom( typeof( TextReader ) ).MustRespectNamingConvention( "*Reader" );
        }
    }

    // The naming convention is broken.
    internal class TextLoader : TextReader { }
}
