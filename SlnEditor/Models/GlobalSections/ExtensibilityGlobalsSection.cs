using System;

namespace SlnEditor.Models.GlobalSections
{
    public class ExtensibilityGlobalsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;

        public Guid? SolutionGuid { get; set; }
    }
}
