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
                new SolutionFolder(new Guid("9B6F52EA-B890-443D-BA30-4422FB5F0BBF"), name: "foo-project", path: "foo/"));

            // Assert
            solution.ToString().Should().Be(@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""foo-project"", ""foo/"", ""{9B6F52EA-B890-443D-BA30-4422FB5F0BBF}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
EndGlobal
");
        }

    }
}
