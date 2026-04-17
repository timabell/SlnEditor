using System;
using System.Collections.Generic;
using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    public class ExtensibilityGlobalsSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        public Guid? SolutionGuid { get; set; }

        /// <summary>
        /// Additional key-value entries in the ExtensibilityGlobals section
        /// that are not specifically modelled (e.g. EnterpriseLibraryConfigurationToolBinariesPath).
        /// </summary>
        public IList<string> AdditionalEntries { get; set; } = new List<string>();

        public string Render()
        {
            if (SolutionGuid == null && AdditionalEntries.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(ExtensibilityGlobals) = postSolution");
            foreach (var entry in AdditionalEntries)
            {
                sb.AppendLine($"\t\t{entry}");
            }
            if (SolutionGuid != null)
            {
                sb.AppendLine($"\t\tSolutionGuid = {{{SolutionGuid.ToString().ToUpper()}}}");
            }
            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
