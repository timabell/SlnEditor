using System.Collections.Generic;

namespace SlnEditor.Models.GlobalSections
{
    public class ConfigurationPlatformsSection : IGlobalSection
    {
        internal int SourceLine { get; set; }
        int ISourceLine.SourceLine => SourceLine;

        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; } = new List<ConfigurationPlatform>();
    }
}
