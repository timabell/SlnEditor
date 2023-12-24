using System.Collections.Generic;

namespace SlnEditor.Models.GlobalSections
{
    public class ConfigurationPlatformsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;

        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; } = new List<ConfigurationPlatform>();
    }
}
