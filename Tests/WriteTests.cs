using FluentAssertions;
using SlnEditor.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlnEditor.Tests;

public class WriteTests
{
    [Fact]
    public void Should_RenderNewSolution()
    {
        var solution = new Solution();
        solution.ToString().Should().Be(@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31410.414
MinimumVisualStudioVersion = 10.0.40219.1
Global
EndGlobal
");
    }

    [Fact]
    public void ShouldRenderHierarchy()
    {
        // Arrange

        var childFolder = new SolutionFolder(new Guid("C0416105-2F65-48CA-A0DD-B2BCFF83E14C"), "child")
        {
            Files = new List<string> { "foo/bar/baz.txt", "hello.json", }
        };

        var solutionFolder = new SolutionFolder(new Guid("87E83066-1C4A-4AAB-B83B-1D4772DF1AA0"), "sln")
        {
            Projects = new List<IProject>
            {
                new Project(new Guid("C733C27E-0A49-4C54-B3D9-96ED900E5563"), "proj", "proj.csproj", ProjectType.CSharp),
                childFolder,
            },
        };

        var solution = new Solution { RootProjects = new List<IProject> { solutionFolder } };

        solution.ToString().Should().Be(@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31410.414
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""sln"", ""sln"", ""{87E83066-1C4A-4AAB-B83B-1D4772DF1AA0}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""proj"", ""proj.csproj"", ""{C733C27E-0A49-4C54-B3D9-96ED900E5563}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""child"", ""child"", ""{C0416105-2F65-48CA-A0DD-B2BCFF83E14C}""
	ProjectSection(SolutionItems) = preProject
		foo/bar/baz.txt = foo/bar/baz.txt
		hello.json = hello.json
	EndProjectSection
EndProject
Global
	GlobalSection(NestedProjects) = preSolution
		{C733C27E-0A49-4C54-B3D9-96ED900E5563} = {87E83066-1C4A-4AAB-B83B-1D4772DF1AA0}
		{C0416105-2F65-48CA-A0DD-B2BCFF83E14C} = {87E83066-1C4A-4AAB-B83B-1D4772DF1AA0}
	EndGlobalSection
EndGlobal
");
    }
}
