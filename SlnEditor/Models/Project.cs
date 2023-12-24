using SlnEditor.Mappings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Header());
            sb.AppendLine("EndProject");
            return sb.ToString();
        }

        public string RenderConfigurations()
        {
            var sb = new StringBuilder();
            foreach (var platform in ConfigurationPlatforms)
            {
                sb.AppendLine(
                    $"\t\t{{{Id.ToString().ToUpper()}}}.{platform.Name} = {platform.Configuration}|{platform.Platform}");
            }

            return sb.ToString();
        }

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";
    }
}
