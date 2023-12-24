using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnEditor.Models.GlobalSections
{
    /// <summary>
    /// Represents the section, data actually lives in <see cref="Project.ConfigurationPlatforms"/>
    /// </summary>
    public class ProjectConfigurationPlatformsSection : IGlobalSection
    {
        private readonly IList<IProject> _projects;
        public int SourceLine { get; internal set; }
        int ISourceLine.SourceLine => SourceLine;

        /// <param name="projects">Reference to solution.Projects - required for rendering</param>
        public ProjectConfigurationPlatformsSection(IList<IProject> projects)
        {
            _projects = projects;
        }

        public string Render()
        {
            if (!_projects.OfType<Project>().Any(p => p.ConfigurationPlatforms.Any()))
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (var project in _projects.OfType<Project>())
            {
                sb.Append(project.RenderConfigurations());
            }

            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
