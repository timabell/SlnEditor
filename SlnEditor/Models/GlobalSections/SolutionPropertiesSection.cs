namespace SlnEditor.Models.GlobalSections
{
    public class SolutionPropertiesSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;

        public SolutionProperties SolutionProperties { get; internal set; } = new SolutionProperties();
    }
}
