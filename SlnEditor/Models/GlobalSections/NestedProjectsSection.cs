using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    public class NestedProjectsSection : IGlobalSection
    {
        private readonly Solution _solution;
        public int SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        /// <param name="solution">Required for rendering as config lives in project list</param>
        public NestedProjectsSection(Solution solution)
        {
            _solution = solution;
        }

        public string Render()
        {
            var allSolutionFolders = _solution.FlatProjectList().OfType<SolutionFolder>().ToList();
            if (!allSolutionFolders.Any(f => f.Projects.Any()))
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(NestedProjects) = preSolution");
            foreach (var solutionFolder in allSolutionFolders)
            {
                sb.Append(solutionFolder.RenderNestedProjects());
            }

            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
