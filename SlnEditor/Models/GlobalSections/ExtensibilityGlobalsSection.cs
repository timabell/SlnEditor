using System;

namespace SlnEditor.Models.GlobalSections
{
    public class ExtensibilityGlobalsSection : IGlobalSection
    {
        internal int SourceLine { get; set; }
        int ISourceLine.SourceLine => SourceLine;

        public Guid? SolutionGuid { get; set; }
    }
}
