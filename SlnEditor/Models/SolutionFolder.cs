using SlnEditor.Mappings;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SlnEditor.Models
{
    /// <summary>
    /// A Solution Folder that can be contained in a <see cref="Solution" />.
    /// Can contain files, <see cref="Project"/>s and other SolutionFolders.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SolutionFolder : IProject
    {
        public SolutionFolder(Guid id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        /// <summary>
        /// The contained <see cref="IProject" />s in the Solution Folder
        /// </summary>
        public IList<IProject> Projects { get; } = new List<IProject>();

        /// <summary>
        /// The contained files in the Solution Folder.
        /// The string value is the relative path to the file.
        /// The path separator *must* be windows format backslashes ('\')
        /// regardless of platform.
        /// </summary>
        public IList<string> Files { get; } = new List<string>();

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Path { get; }

        /// <inheritdoc />
        public Guid TypeGuid => new ProjectTypeMap().Guids[Type];

        /// <inheritdoc />
        public ProjectType Type => ProjectType.SolutionFolder;

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";
    }
}
