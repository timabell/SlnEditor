namespace SlnEditor.Models.GlobalSections
{
    public class SolutionPropertiesSection : IGlobalSection
    {
        internal int SourceLine { get; set; }
        int ISourceLine.SourceLine => SourceLine;

        public SolutionProperties SolutionProperties { get; internal set; } = new SolutionProperties();
    }
}
