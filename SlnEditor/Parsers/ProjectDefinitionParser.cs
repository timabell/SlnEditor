using SlnEditor.Models;
using System;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal sealed class ProjectDefinitionParser : IParseProjectDefinition
    {
        private readonly ProjectTypeMap _projectTypeMapMapper = new ProjectTypeMap();

        public bool TryParseProjectDefinition(
            Solution solution,
            string projectDefinition,
            out IProject? project)
        {
            project = null;

            if (!projectDefinition.StartsWith("Project(\"{")) return false;

            // c.f.: https://regexr.com/650df
            const string pattern =
                @"Project\(""\{(?<projectTypeGuid>[A-Za-z0-9\-]+)\}""\) = ""(?<projectName>.+)"", ""(?<projectPath>.+)"", ""\{(?<projectGuid>[A-Za-z0-9\-]+)\}";
            var match = Regex.Match(projectDefinition, pattern);
            if (!match.Success) return false;

            var projectTypeGuidString = match.Groups["projectTypeGuid"].Value;
            var projectName = match.Groups["projectName"].Value;
            var projectPath = match.Groups["projectPath"].Value;
            var projectGuidString = match.Groups["projectGuid"].Value;

            var projectTypeGuid = Guid.Parse(projectTypeGuidString);
            var projectGuid = Guid.Parse(projectGuidString);

            var projectType = _projectTypeMapMapper.Types[projectTypeGuid];

            project = projectType == ProjectType.SolutionFolder
                ? (IProject)new SolutionFolder(
                    projectGuid,
                    projectName,
                    projectPath,
                    projectTypeGuid,
                    projectType)
                : new SolutionProject(
                    projectGuid,
                    projectName,
                    projectPath,
                    projectTypeGuid,
                    projectType);

            return true;
        }
    }
}
