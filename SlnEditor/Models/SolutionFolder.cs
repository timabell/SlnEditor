using SlnEditor.Mappings;
using SlnEditor.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SlnEditor.Models
{
    /// <summary>
    ///     A Solution Folder that can be contained in a <see cref="Solution" />
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SolutionFolder : IProject
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SolutionFolder" />
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="name">The name</param>
        /// <param name="path"></param>
        /// <param name="typeGuid">The project-type id</param>
        /// <param name="type">The well-known project-type</param>
        public SolutionFolder(
            Guid id,
            string name,
            string path,
            Guid typeGuid,
            ProjectType type)
        {
            Id = id;
            Name = name;
            Path = path;
            TypeGuid = typeGuid;
            Type = type;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SolutionFolder" />
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="name">The name</param>
        /// <param name="path"></param>
        /// <param name="type">The well-known project-type</param>
        public SolutionFolder(
            Guid id,
            string name,
            string path,
            ProjectType type)
        {
            Id = id;
            Name = name;
            Path = path;
            TypeGuid = new ProjectTypeMap().Guids[type];
            Type = type;
        }

        /// <summary>
        ///     The contained <see cref="IProject" />s in the Solution Folder
        /// </summary>
        public IList<IProject> Projects { get; } = new List<IProject>();

        /// <summary>
        ///     The contained <see cref="FileInfo" />s in the Solution Folder
        /// </summary>
        public IList<string> Files { get; } = new List<string>();

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

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";
    }
}
