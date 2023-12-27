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
            const string emptySln = @"
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
            solution.RootProjects.Add(
                new SolutionFolder(new Guid("9B6F52EA-B890-443D-BA30-4422FB5F0BBF"), name: "foo-project"));

            // Assert
            solution.ToString().Should().Be(@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""foo-project"", ""foo-project"", ""{9B6F52EA-B890-443D-BA30-4422FB5F0BBF}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
EndGlobal
");
        }

        [Fact]
        public void Should_RenderNestedSolutionFolder()
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
            var solutionItemsFolder = new SolutionFolder(new Guid("4EA04FF3-B1C7-446E-A9D8-5041715C7F29"), name: "SolutionItems");
            solution.RootProjects.Add(solutionItemsFolder);

            var parentFolder = new SolutionFolder(new Guid("3B2E7276-2FF3-4DF1-ACB3-5243D4F9BBE1"), name: "parent-folder");
            solutionItemsFolder.Projects.Add(parentFolder);

            var childFolder = new SolutionFolder(new Guid("9E5901F5-0E50-41CD-81C2-3E130F124F92"), name: "child-folder");
            parentFolder.Projects.Add(childFolder);

            // Assert
            solution.ToString().Should().Be(@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""SolutionItems"", ""SolutionItems"", ""{4EA04FF3-B1C7-446E-A9D8-5041715C7F29}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""parent-folder"", ""parent-folder"", ""{3B2E7276-2FF3-4DF1-ACB3-5243D4F9BBE1}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""child-folder"", ""child-folder"", ""{9E5901F5-0E50-41CD-81C2-3E130F124F92}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{3B2E7276-2FF3-4DF1-ACB3-5243D4F9BBE1} = {4EA04FF3-B1C7-446E-A9D8-5041715C7F29}
		{9E5901F5-0E50-41CD-81C2-3E130F124F92} = {3B2E7276-2FF3-4DF1-ACB3-5243D4F9BBE1}
	EndGlobalSection
EndGlobal
");
        }
    }
}
