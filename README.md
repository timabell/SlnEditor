# SlnEditor

<https://github.com/timabell/SlnEditor>

This fork of [https://github.com/wgnf/SlnParser/](https://github.com/wgnf/SlnParser/) adds write support, allowing a full round-trip of the sln file to disk or string.

[![Latest Release](https://img.shields.io/nuget/v/SlnEditor)](https://www.nuget.org/packages/SlnEditor/)
[![Downloads](https://img.shields.io/nuget/dt/SlnEditor)](https://www.nuget.org/packages/SlnEditor/)  

Easy (to use) Editor for your .NET Solution (.sln) Files.

This project targets `netstandard2.0` so it can basically be used anywhere you want.

## License: MIT

The library is licensed under the MIT license.

Referenced nuget packages remain under their respective licenses.

The tests include an apache-2 licensed example sln which remains under that license.

## 💻 Usage

### Modifying a sln

```cs
var solution = new Solution("path/to/your/solution.sln");
solution.Projects.Add(new SolutionFolder(name: "A Sln Folder")
{
    Files = new List<string> { "path/to/file.txt"},
    Projects = new List<IProject>
    {
        new Project("MyCode", "code.csproj", ProjectType.CSharp),
    },
};
var modifiedSln = solution.ToString();
File.WriteAllText("path/to/your/solution.sln", modifiedSln);
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

### sln-items-sync

- <https://github.com/timabell/sln-items-sync>

sln-items-sync uses this SlnEditor nuget package so might be a good example of using this library.

sln-items-sync is the reason this SlnEditor fork of SlnParser exists at all.

### Stackoverflow

The usual host of people asking on stackoverflow, with mixed responses for this one.

- <https://stackoverflow.com/questions/707107/parsing-visual-studio-solution-files>
- <https://stackoverflow.com/questions/8742316/how-to-create-visual-studio-solution-sln-files-programmatically-including-we>
- <https://stackoverflow.com/questions/2736260/programmatically-generate-visual-studio-solution>
- <https://stackoverflow.com/questions/14153614/how-to-generate-a-new-visual-studio-project-in-a-visual-studio-project-programma>

# Developer setup

You can either build/run/test this locally or in [dev containers](https://containers.dev/)

You should be able to use the dev tools of your choice - VSCode, Rider, Visual Studio etc
