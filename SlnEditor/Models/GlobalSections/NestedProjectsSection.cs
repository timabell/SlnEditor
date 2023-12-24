using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    public class NestedProjectsSection : IGlobalSection
    {
        private readonly IList<IProject> _projects;
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;

        /// <param name="projects">Reference to solution.Projects - required for rendering</param>
        public NestedProjectsSection(IList<IProject> projects)
        {
            _projects = projects;
        }

        public string Render()
        {
            if (!_projects.OfType<SolutionFolder>().Any(f => f.Projects.Any()))
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(NestedProjects) = preSolution");
            foreach (var solutionFolder in _projects.OfType<SolutionFolder>())
            {
                sb.Append(solutionFolder.RenderNestedProjects());
            }

            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
