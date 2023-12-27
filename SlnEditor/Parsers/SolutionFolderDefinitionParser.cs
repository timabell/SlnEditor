using SlnEditor.Exceptions;
using SlnEditor.Mappings;
using SlnEditor.Models;
using System;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class SolutionFolderDefinitionParser
    {
        private readonly ProjectTypeMap _projectTypeMapMapper = new ProjectTypeMap();

        /// <summary>
        /// If current line contains a solution folder definition return a new solution folder project,
        /// else returns null.
        /// </summary>
        public SolutionFolder? Parse(string line, int lineNumber)
        {
            var match = Regex.Match(line,
                @"Project\(""\{(?<projectTypeGuid>[A-Za-z0-9\-]+)\}""\) = ""(?<projectName>.+)"", ""(?<projectPath>.+)"", ""\{(?<projectGuid>[A-Za-z0-9\-]+)\}");
            if (!match.Success)
            {
                return null;
            }

            var typeGuid = Guid.Parse(match.Groups["projectTypeGuid"].Value);
            if (_projectTypeMapMapper.Types[typeGuid] != ProjectType.SolutionFolder)
            {
                return null;
            }

            return new SolutionFolder(
                id: Guid.Parse(match.Groups["projectGuid"].Value),
                name: match.Groups["projectName"].Value,
                lineNumber);
        }

        /// <summary>
        /// If current line contains a solution folder definition return a new solution folder project,
        /// else returns null.
        /// </summary>
        public static string? ParseFile(string line, int lineNumber, bool bestEffort)
        {
            var match = Regex.Match(line, @"(?<key>[\S]+)\s*=\s*(?<value>[\S]+)");
            if (!match.Success)
            {
                return null;
            }

            if (!bestEffort && match.Groups["key"].Value != match.Groups["value"].Value)
            {
                throw new UnexpectedSolutionStructureException(
                    $"Unexpected solution file format. Expected 'path/to/file.txt = path/to/file.txt'. Line {lineNumber}.");
            }

            return match.Groups["key"].Value;
        }
    }
}
