// Warning MY001 on `Writer`: `The field SomeType.Writer must be private.`
//    CodeFix: Make private`
using System.IO;

namespace Doc.ProjectValidatorWithFix
{
    class SomeType
    {
        public TextWriter Writer;
    }

}
