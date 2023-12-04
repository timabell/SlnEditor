# SlnEditor

<https://github.com/timabell/SlnEditor>

This fork of [https://github.com/wgnf/SlnParser/](https://github.com/wgnf/SlnParser/) adds write support, allowing a full round-trip of the sln file to disk or string.

[![GitHub license](https://img.shields.io/badge/Unlicense-blue.svg)](LICENSE)
[![Uses SemVer 2.0.0](https://img.shields.io/badge/Uses%20SemVer-2.0.0-green)](https://semver.org/spec/v2.0.0.html)
[![Latest Release](https://img.shields.io/nuget/v/SlnEditor)](https://www.nuget.org/packages/SlnEditor/)
[![Downloads](https://img.shields.io/nuget/dt/SlnEditor)](https://www.nuget.org/packages/SlnEditor/)  
[![GitHub stars](https://img.shields.io/github/stars/OptiSchmopti/CsvProc9000?style=social)](https://github.com/OptiSchmopti/CsvProc9000/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/OptiSchmopti/CsvProc9000?style=social)](https://github.com/OptiSchmopti/CsvProc9000/network/members)
[![GitHub watchers](https://img.shields.io/github/watchers/OptiSchmopti/CsvProc9000?style=social)](https://github.com/OptiSchmopti/CsvProc9000/watchers)  

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
