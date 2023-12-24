using FluentAssertions;
using SlnEditor.Models;
using System;
using Xunit;

namespace SlnEditor.Tests
{
    public class MutateTests
    {
        [Fact]
        public void Should_RenderAddedSolutionFolder()
        {
            var emptySln = @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
	EndGlobalSection
EndGlobal
";

            // Arrange
            var solution = new Solution(emptySln);

            // Act
            solution.Projects.Add(
                new SolutionFolder(Guid.NewGuid(), name: "foo-project", path: "foo/"));

            // Assert
            solution.ToString().Should().Contain("foo-project");
        }

    }
}
