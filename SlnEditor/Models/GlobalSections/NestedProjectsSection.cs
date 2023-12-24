namespace SlnEditor.Models.GlobalSections
{
    public class NestedProjectsSection : IGlobalSection
    {
        internal int SourceLine { get; set; }
        int ISourceLine.SourceLine => SourceLine;
    }
}
