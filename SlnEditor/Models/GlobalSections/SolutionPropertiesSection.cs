using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    public class SolutionPropertiesSection : IGlobalSection
    {
        public int SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        public SolutionProperties SolutionProperties { get; internal set; } = new SolutionProperties();

        public string Render()
        {
            if (!SolutionProperties.HideSolutionNode.HasValue)
            {
                return "";
            }
            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(SolutionProperties) = preSolution");
            sb.AppendLine($"\t\tHideSolutionNode = {SolutionProperties.HideSolutionNode.ToString().ToUpper()}");
            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
