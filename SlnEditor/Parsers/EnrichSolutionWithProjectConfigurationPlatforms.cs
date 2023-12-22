﻿using SlnEditor.Exceptions;
using SlnEditor.Models;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithProjectConfigurationPlatforms : IEnrichSolution
    {
        private readonly SolutionConfigurationPlatformParser _solutionConfigurationPlatformParser = new SolutionConfigurationPlatformParser();

        public void Enrich(Solution solution, IList<string> fileContents)
        {
            var projectConfigurations = _solutionConfigurationPlatformParser.Parse(
                fileContents,
                "ProjectConfiguration");
            MapConfigurationPlatformsToProjects(solution, projectConfigurations);
        }

        private static void MapConfigurationPlatformsToProjects(
            ISolution solution,
            IList<ProjectConfigurationPlatform> projectConfigurations)
        {
            foreach (var configuration in projectConfigurations)
                MapConfigurationPlatformToProject(solution, configuration);
        }

        private static void MapConfigurationPlatformToProject(
            ISolution solution,
            ProjectConfigurationPlatform configuration)
        {
            if (!configuration.ProjectId.HasValue)
                throw new UnexpectedSolutionStructureException(
                    "Expected to find a project-id " +
                    $"for the Project-Platform-Configuration '{configuration.ConfigurationPlatform.Name}'");

            var project = solution
                .AllProjects
                .FirstOrDefault(project => project.Id == configuration.ProjectId.Value);

            if (project == null) return;

            if (!(project is SolutionProject solutionProject))
                throw new UnexpectedSolutionStructureException(
                    "Expected to find a Solution-Project with the id " +
                    $"'{configuration.ProjectId.Value}' for the Project-Platform-Configuration " +
                    $"'{configuration.ConfigurationPlatform.Name}' but found " +
                    $" project of type '{project.GetType().Name}' instead");

            solutionProject.AddConfigurationPlatform(configuration.ConfigurationPlatform);
        }
    }
}