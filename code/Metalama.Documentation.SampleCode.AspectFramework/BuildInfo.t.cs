namespace Doc.BuildInfo
{
#pragma warning disable CS0067
    internal partial class BuildInfo
    {


        private string? _configuration = "Debug";

        public string? Configuration
        {
            get
            {
                return this._configuration;
            }
        }

        private string? _targetFramework = "net6.0";

        public string? TargetFramework
        {
            get
            {
                return this._targetFramework;
            }
        }

    }
#pragma warning restore CS0067
}
