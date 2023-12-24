namespace SlnEditor.Models.GlobalSections
{
    /// <summary>
    /// Represents the section, data actually lives in <see cref="Project.ConfigurationPlatforms"/>
    /// </summary>
    public class ProjectConfigurationPlatformsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;
    }
}
