using SlnEditor.Exceptions;
using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithProjectConfigurationPlatforms : IEnrichSolution
    {
        private readonly ProjectConfigurationPlatformParser _parser = new ProjectConfigurationPlatformParser();

        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            const string sectionNamePrefix = "ProjectConfiguration";
            var section = _parser.Parse(fileContents, sectionNamePrefix, solution);

            solution.GlobalSections.Add(new ProjectConfigurationPlatformsSection(solution)
            {
                SourceLine = section.SourceLine,
            });
            MapConfigurationPlatformsToProjects(solution, section.ProjectConfigurationPlatforms);
        }

        private static void MapConfigurationPlatformsToProjects(Solution solution,
            IEnumerable<ProjectConfigurationPlatform> projectConfigurations)
        {
            foreach (var configuration in projectConfigurations)
            {
                MapConfigurationPlatformToProject(solution, configuration);
            }
        }

        private static void MapConfigurationPlatformToProject(
            Solution solution,
            ProjectConfigurationPlatform configuration)
        {
            if (!configuration.ProjectId.HasValue)
            {
                throw new UnexpectedSolutionStructureException(
                    "Expected to find a project-id " +
                    $"for the Project-Platform-Configuration '{configuration.ConfigurationPlatform.Name}'");
            }

            var project = solution
                .FlatProjectList()
                .FirstOrDefault(project => project.Id == configuration.ProjectId.Value);

            if (project == null)
            {
                return;
            }

            if (!(project is Project solutionProject))
            {
                throw new UnexpectedSolutionStructureException(
                    "Expected to find a Solution-Project with the id " +
                    $"'{configuration.ProjectId.Value}' for the Project-Platform-Configuration " +
                    $"'{configuration.ConfigurationPlatform.Name}' but found " +
                    $" project of type '{project.GetType().Name}' instead");
            }

            solutionProject.ConfigurationPlatforms.Add(configuration.ConfigurationPlatform);
        }
    }
}
