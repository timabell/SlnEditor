using SlnEditor.Parsers;
using System;
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
        private readonly Solution _solution;
        public int SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        /// <summary>
        /// Only for passing data while parsing, not for users of the library.
        /// Users should use <see cref="Project.ConfigurationPlatforms"/> instead.
        /// </summary>
        internal IList<ProjectConfigurationPlatform> ProjectConfigurationPlatforms { get; set; }

        /// <param name="solution">Required for rendering as config lives in project list</param>
        public ProjectConfigurationPlatformsSection(Solution solution)
        {
            _solution = solution;
        }

        public string Render()
        {
            var projects = _solution.FlatProjectList().OfType<Project>().ToList();
            if (!projects.Any(p => p.ConfigurationPlatforms.Any()))
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            var configs = projects.SelectMany(project => project.ConfigurationPlatforms,
                (project, config) => new { ProjectId = project.Id, Config = config })
                .ToList();
            foreach (var projectConfig in configs.OrderBy(c => c.Config.SourceLine ?? int.MaxValue)) // put new entries on end
            {
                sb.AppendLine(projectConfig.Config.Render(projectConfig.ProjectId));
            }

            sb.AppendLine("\tEndGlobalSection");
            return sb.ToString();
        }
    }
}
