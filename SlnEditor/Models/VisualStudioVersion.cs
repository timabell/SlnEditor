using System.Text;

namespace SlnEditor.Models
{
    /// <summary>
    ///     A Visual Studio Version
    /// </summary>
    public class VisualStudioVersion
    {
        /// <summary>
        ///     The actually used Version of Visual Studio
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        ///     The minimum Version of Visual Studio that is compatible
        /// </summary>
        public string MinimumVersion { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string MajorVersion => Version.Contains(".") ? Version.Substring(0, Version.IndexOf('.')) : Version;

        public string Render()
        {
            var sb = new StringBuilder();
            switch (MajorVersion)
            {
                case "2008":
                case "15":
                    sb.AppendLine($"# Visual Studio {MajorVersion}");
                    break;
                default:
                    sb.AppendLine($"# Visual Studio Version {MajorVersion}");
                    break;
            }
            sb.AppendLine($"VisualStudioVersion = {Version}");
            sb.AppendLine($"MinimumVisualStudioVersion = {MinimumVersion}");
            return sb.ToString();
        }
    }
}
