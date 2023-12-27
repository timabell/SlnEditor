using FluentAssertions;
using SlnEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SlnEditor.Tests;

public class FlattenerTests
{
    [Fact]
    public void FlattensHierarchy()
    {
        const string slnFolderName = nameof(slnFolderName);
        const string projName = nameof(projName);
        const string childFolderName = nameof(childFolderName);
        const string innerProjName = nameof(innerProjName);
        const string innerChildFolderName = nameof(innerChildFolderName);
        var solution = new Solution
        {
            RootProjects = new List<IProject>
            {
                new SolutionFolder(slnFolderName)
                {
                    Projects = new List<IProject>
                    {
                        new Project(projName, "proj.csproj", ProjectType.CSharp),
                        new SolutionFolder(childFolderName)
                        {
                            Files = new List<string> { "foo/bar/baz.txt", "hello.json", },
                            Projects = new List<IProject>
                            {
                                new Project(innerProjName, "other.csproj",
                                    ProjectType.CSharp2),
                                new SolutionFolder(innerChildFolderName)
                                {
                                    Files = new List<string> { "goodbye.md" },
                                },
                            },
                        },
                    },
                },
            },
        };

        solution.FlatProjectList().Select(p => p.Name).Should().BeEquivalentTo(new[]
        {
            slnFolderName,
            projName,
            childFolderName,
            innerProjName,
            innerChildFolderName,
        });
    }
}
