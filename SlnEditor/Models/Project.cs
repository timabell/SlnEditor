using SlnEditor.Mappings;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SlnEditor.Models
{
    /// <summary>
    /// A project that can be contained in a <see cref="Solution" />.
    /// Can be one of any of the many supported types.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Project : IProject
    {
        public Project(Guid id, string name, string path, ProjectType type)
        {
            Id = id;
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

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";
    }
}
