namespace SlnEditor.Models.GlobalSections
{
    /// <summary>
    /// Represents the section, data actually lives in <see cref="Project.ConfigurationPlatforms"/>
    /// </summary>
    public class ProjectConfigurationPlatformsSection : IGlobalSection
    {
        internal int SourceLine { get; set; }
        int ISourceLine.SourceLine => SourceLine;
    }
}
