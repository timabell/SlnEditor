using SlnEditor.Exceptions;
using SlnEditor.Mappings;
using SlnEditor.Models;
using System;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class ProjectDefinitionParser
    {
        private readonly ProjectTypeMap _projectTypeMapMapper = new ProjectTypeMap();

        /// <summary>
        /// If current line contains a project definition return a new project,
        /// else returns null.
        /// </summary>
        /// <returns></returns>
        public IProject? Parse(string line, int lineNumber)
        {
            var match = Regex.Match(line, @"Project\(""\{(?<projectTypeGuid>[A-Za-z0-9\-]+)\}""\) = ""(?<projectName>.+)"", ""(?<projectPath>.+)"", ""\{(?<projectGuid>[A-Za-z0-9\-]+)\}");
            if (!match.Success)
            {
                return null;
            }

            var typeGuid = Guid.Parse(match.Groups["projectTypeGuid"].Value);
            if (!_projectTypeMapMapper.Types.ContainsKey(typeGuid))
            {
                throw new UnexpectedSolutionStructureException($"Unknown project type guid {typeGuid}.");
            }
            var projectType = _projectTypeMapMapper.Types[typeGuid];
            if (projectType == ProjectType.SolutionFolder)
            {
                return null;
            }

            var projectGuidString = match.Groups["projectGuid"].Value;

            return new Project(
                id: Guid.Parse(projectGuidString),
                name: match.Groups["projectName"].Value,
                path: match.Groups["projectPath"].Value,
                type: projectType,
                lineNumber);
        }
    }
}
