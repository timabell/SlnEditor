namespace SlnEditor.Models.GlobalSections
{
    public class NestedProjectsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;
    }
}
