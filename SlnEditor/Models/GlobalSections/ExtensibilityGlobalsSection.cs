using System;
using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    public class ExtensibilityGlobalsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        public Guid? SolutionGuid { get; set; }

        public string Render()
        {
            if (SolutionGuid == null)
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(ExtensibilityGlobals) = postSolution");
            sb.AppendLine($"\t\tSolutionGuid = {{{SolutionGuid.ToString().ToUpper()}}}");
            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
