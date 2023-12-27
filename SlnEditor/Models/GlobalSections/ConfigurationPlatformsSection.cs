using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    public class ConfigurationPlatformsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; } =
            new List<ConfigurationPlatform>();

        public string Render()
        {
            if (!ConfigurationPlatforms.Any())
            {
                return "";
            }

            var sb = new StringBuilder();

            sb.AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            foreach (var platform in ConfigurationPlatforms)
            {
                sb.AppendLine(platform.Render());
            }

            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
