# SlnEditor

<https://github.com/timabell/SlnEditor>

This fork of [https://github.com/wgnf/SlnParser/](https://github.com/wgnf/SlnParser/) adds write support, allowing a full round-trip of the sln file to disk or string.

[![GitHub license](https://img.shields.io/badge/Unlicense-blue.svg)](LICENSE)
[![Uses SemVer 2.0.0](https://img.shields.io/badge/Uses%20SemVer-2.0.0-green)](https://semver.org/spec/v2.0.0.html)
[![Latest Release](https://img.shields.io/nuget/v/SlnEditor)](https://www.nuget.org/packages/SlnEditor/)
[![Downloads](https://img.shields.io/nuget/dt/SlnEditor)](https://www.nuget.org/packages/SlnEditor/)  

Easy (to use) Editor for your .NET Solution (.sln) Files.

This project targets `netstandard2.0` so it can basically be used anywhere you want. I've not yet run any performance tests.

## 💻 Usage

### Modifying a sln

```cs
var solution = new SolutionParser().Parse("path/to/your/solution.sln");
solution.Projects.Add(new SolutionFolder(Guid.NewGuid(), name: "foo-project", path: "foo/", typeGuid: new ProjectTypeMapper().ToGuid(ProjectType.Test), ProjectType.Test));
string modifiedSln = solution.Write();
File.WriteAllText("path/to/your/solution.sln", actual);
```

### Accessing the projects

```cs

// gives you a flat list of all the Projects/Solution-Folders in your Solution
var flat = parsedSolution.AllProjects;

// gives you a structured (Solution-Folders containing projects) of all the Projects/Solution-Folders in your solution
var structured = parsedSolution.Projects;

// this'll give you all the projects that are not a Solution-Folder
var noFolders = parsedSolution
  .AllProjects
  .OfType<SolutionProject>();

// this'll give you all the projects of the desired type (C# class libs in this case)
var csharpProjects = parsedSolution
  .AllProjects
  .Where(project => project.Type == ProjectType.CSharpClassLibrary);
```
## Other sln editing tools

### dotnet sln

There's a `dotnet new sln` and `dotnet sln` which provides some create/edit capabilities - see <https://andrewlock.net/creating-and-editing-solution-files-with-the-net-cli/>

### SlnTools

There's SlnTools <https://www.nuget.org/packages/SLNTools.Core> / <https://github.com/mtherien/slntools>

### Stackoverflow

The usual host of people asking on stackoverflow, with mixed responses for this one.

- <https://stackoverflow.com/questions/707107/parsing-visual-studio-solution-files>
- <https://stackoverflow.com/questions/8742316/how-to-create-visual-studio-solution-sln-files-programmatically-including-we>
- <https://stackoverflow.com/questions/2736260/programmatically-generate-visual-studio-solution>
- <https://stackoverflow.com/questions/14153614/how-to-generate-a-new-visual-studio-project-in-a-visual-studio-project-programma>
