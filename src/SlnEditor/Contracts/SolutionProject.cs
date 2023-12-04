using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SlnEditor.Contracts
{
    /// <summary>
    ///     A Solution Project that can be contained in a <see cref="Solution" />
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SolutionProject : IProject
    {
        private readonly IList<ConfigurationPlatform> _configurationPlatforms =
            new List<ConfigurationPlatform>();

        /// <summary>
        ///     Creates a new instance of <see cref="SolutionProject" />
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="name">The name</param>
        /// <param name="path"></param>
        /// <param name="typeGuid">The project-type id</param>
        /// <param name="type">The well-known project-type</param>
        /// <param name="fileInfo">The <see cref="FileInfo" /> for the Project-File</param>
        public SolutionProject(
            Guid id,
            string name,
            string path,
            Guid typeGuid,
            ProjectType type,
            FileInfo fileInfo)
        {
            Id = id;
            Name = name;
            Path = path;
            TypeGuid = typeGuid;
            Type = type;
            File = fileInfo;
        }

        /// <summary>
        ///     The File of the Project
        /// </summary>
        public FileInfo File { get; }

        /// <summary>
        ///     The <see cref="ConfigurationPlatform" />s configured for this solution
        /// </summary>
        public IList<ConfigurationPlatform> ConfigurationPlatforms =>
            _configurationPlatforms.ToList();

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Path { get; }

        /// <inheritdoc />
        public Guid TypeGuid { get; }

        /// <inheritdoc />
        public ProjectType Type { get; }

        internal void AddConfigurationPlatform(ConfigurationPlatform configurationPlatform)
        {
            if (configurationPlatform == null) throw new ArgumentNullException(nameof(configurationPlatform));

            _configurationPlatforms.Add(configurationPlatform);
        }

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";
    }
}
