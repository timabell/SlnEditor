using SlnEditor.Mappings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SlnEditor.Models
{
    /// <summary>
    /// A project that can be contained in a <see cref="Solution" />.
    /// Can be one of any of the many supported types apart from SolutionFolder is represented by <seealso cref="SolutionFolder"/>.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Project : IProject
    {
        internal Project(Guid id, string name, string path, ProjectType type, int lineNumber)
        {
            Id = id;
            Name = name;
            Path = path;
            Type = type;
            SourceLine = lineNumber;
        }

        public Project(Guid id, string name, string path, ProjectType type)
        {
            Id = id;
            Name = name;
            Path = path;
            Type = type;
        }

        public Project(string name, string path, ProjectType type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = path;
            Type = type;
        }

        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; } = new List<ConfigurationPlatform>();

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Path { get; }

        /// <inheritdoc />
        public Guid TypeGuid => new ProjectTypeMap().Guids[Type];

        /// <inheritdoc />
        public ProjectType Type { get; }

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Header());
            sb.AppendLine("EndProject");
            return sb.ToString();
        }

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";
        public int? SourceLine { get; }
    }
}
