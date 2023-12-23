using FluentAssertions;
using SlnEditor.Models;
using System;
using System.IO;
using Xunit;

namespace SlnEditor.Tests
{
    public class WriteTests
    {
        private const string SlnContentsSlnParser = @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31410.414
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""SlnParser"", ""SlnParser\SlnParser.csproj"", ""{EDC2B9FC-02D0-4541-8484-CAB27B00252D}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""Solution Items"", ""Solution Items"", ""{6D0A7ECB-8812-42C3-8CB4-3DD2C8296591}""
	ProjectSection(SolutionItems) = preProject
		.editorconfig = .editorconfig
	EndProjectSection
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SlnParser.Tests"", ""SlnParser.Tests\SlnParser.Tests.csproj"", ""{BB52A27D-766E-4ECD-B888-BD86405134C1}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|x64 = Debug|x64
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|x64 = Release|x64
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x64.ActiveCfg = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x64.Build.0 = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x86.ActiveCfg = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x86.Build.0 = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|Any CPU.Build.0 = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x64.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x64.Build.0 = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x86.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x86.Build.0 = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x64.ActiveCfg = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x64.Build.0 = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x86.ActiveCfg = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x86.Build.0 = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|Any CPU.Build.0 = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x64.ActiveCfg = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x64.Build.0 = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x86.ActiveCfg = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x86.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {7F92F20E-4C3D-4316-BF60-105559EFEAFF}
	EndGlobalSection
EndGlobal
";

        private const string SlnContentsTestSln = @"

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.26124.0
MinimumVisualStudioVersion = 15.0.26124.0
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""SolutionFolder1"", ""SolutionFolder1"", ""{DD601D16-308D-4B76-AB4B-2AA5B1D25876}""
	ProjectSection(SolutionItems) = preProject
		something\something.txt = something\something.txt
		test123.txt = test123.txt
		test456.txt = test456.txt
	EndProjectSection
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""NestedSolutionFolder"", ""NestedSolutionFolder"", ""{DA01EB1C-A2F7-4851-AD58-D1319B29EE3D}""
	ProjectSection(SolutionItems) = preProject
		testNested1.txt = testNested1.txt
	EndProjectSection
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""ClassLibraryNestedSolutionFolder"", ""ClassLibraryNestedSolutionFolder\ClassLibraryNestedSolutionFolder.csproj"", ""{02A963A7-1C87-4B72-AF6A-100EE093E97E}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""ClassLibraryInSolutionFolder1"", ""ClassLibraryInSolutionFolder1\ClassLibraryInSolutionFolder1.csproj"", ""{298A6739-2859-4A48-9EB7-55EDD65F4F65}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""ConsoleApp1"", ""ConsoleApp1\ConsoleApp1.csproj"", ""{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""WpfAppNotInASolutionFolder"", ""WpfAppNotInASolutionFolder\WpfAppNotInASolutionFolder.csproj"", ""{A1EDEE87-B967-46AB-9889-DD1704F09918}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""WinFormsApp1"", ""WinFormsApp1\WinFormsApp1.csproj"", ""{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""WebApplication"", ""WebApplication\WebApplication.csproj"", ""{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|x64 = Debug|x64
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|x64 = Release|x64
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Debug|x64.ActiveCfg = Debug|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Debug|x64.Build.0 = Debug|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Debug|x86.ActiveCfg = Debug|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Debug|x86.Build.0 = Debug|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Release|Any CPU.Build.0 = Release|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Release|x64.ActiveCfg = Release|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Release|x64.Build.0 = Release|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Release|x86.ActiveCfg = Release|Any CPU
		{02A963A7-1C87-4B72-AF6A-100EE093E97E}.Release|x86.Build.0 = Release|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Debug|x64.ActiveCfg = Debug|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Debug|x64.Build.0 = Debug|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Debug|x86.ActiveCfg = Debug|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Debug|x86.Build.0 = Debug|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Release|Any CPU.Build.0 = Release|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Release|x64.ActiveCfg = Release|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Release|x64.Build.0 = Release|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Release|x86.ActiveCfg = Release|Any CPU
		{298A6739-2859-4A48-9EB7-55EDD65F4F65}.Release|x86.Build.0 = Release|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Debug|x64.ActiveCfg = Debug|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Debug|x64.Build.0 = Debug|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Debug|x86.ActiveCfg = Debug|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Debug|x86.Build.0 = Debug|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Release|Any CPU.Build.0 = Release|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Release|x64.ActiveCfg = Release|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Release|x64.Build.0 = Release|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Release|x86.ActiveCfg = Release|Any CPU
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A}.Release|x86.Build.0 = Release|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Debug|x64.ActiveCfg = Debug|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Debug|x64.Build.0 = Debug|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Debug|x86.ActiveCfg = Debug|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Debug|x86.Build.0 = Debug|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Release|Any CPU.Build.0 = Release|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Release|x64.ActiveCfg = Release|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Release|x64.Build.0 = Release|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Release|x86.ActiveCfg = Release|Any CPU
		{A1EDEE87-B967-46AB-9889-DD1704F09918}.Release|x86.Build.0 = Release|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Debug|x64.ActiveCfg = Debug|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Debug|x64.Build.0 = Debug|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Debug|x86.ActiveCfg = Debug|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Debug|x86.Build.0 = Debug|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Release|Any CPU.Build.0 = Release|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Release|x64.ActiveCfg = Release|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Release|x64.Build.0 = Release|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Release|x86.ActiveCfg = Release|Any CPU
		{D4E03E9B-EE01-462A-B2F3-45AC775ADC7E}.Release|x86.Build.0 = Release|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Debug|x64.ActiveCfg = Debug|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Debug|x64.Build.0 = Debug|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Debug|x86.ActiveCfg = Debug|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Debug|x86.Build.0 = Debug|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Release|Any CPU.Build.0 = Release|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Release|x64.ActiveCfg = Release|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Release|x64.Build.0 = Release|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Release|x86.ActiveCfg = Release|Any CPU
		{20AD21B6-0ABB-4DB5-8BA0-D9896E58E3E4}.Release|x86.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{DA01EB1C-A2F7-4851-AD58-D1319B29EE3D} = {DD601D16-308D-4B76-AB4B-2AA5B1D25876}
		{298A6739-2859-4A48-9EB7-55EDD65F4F65} = {DD601D16-308D-4B76-AB4B-2AA5B1D25876}
		{8B9C2C31-44F0-4F2E-9A8E-9223CE240A5A} = {DD601D16-308D-4B76-AB4B-2AA5B1D25876}
		{02A963A7-1C87-4B72-AF6A-100EE093E97E} = {DA01EB1C-A2F7-4851-AD58-D1319B29EE3D}
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

        private const string SlnContentsDotnetNew = @"

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.30114.105
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

        private const string SlnContentsHideSolutionNode = @"

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.30114.105
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = TRUE
	EndGlobalSection
EndGlobal
";

        private const string SlnContentsNoProperties = @"

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.30114.105
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
EndGlobal
";

        private const string SlnContentsSlnSync = @"

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""tests"", ""tests\tests.csproj"", ""{9FD4249B-053E-4900-A0AF-834628B8B82A}""
EndProject
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""sln-items-sync"", ""src\sln-items-sync.csproj"", ""{2F916CD2-B6E7-44C3-8B29-5658CA166700}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""SolutionItems"", ""SolutionItems"", ""{F7226E79-E7AF-44D2-8071-A019146DCA06}""
	ProjectSection(SolutionItems) = preProject
		.gitignore = .gitignore
		.tool-versions = .tool-versions
	EndProjectSection
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = "".github"", "".github"", ""{50259428-AA36-4437-AF08-F1B4DFE0D580}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""workflows"", ""workflows"", ""{8397A6CD-B67C-4FBB-9ED9-59C92393BA16}""
	ProjectSection(SolutionItems) = preProject
		.github\workflows\dotnet.yml = .github\workflows\dotnet.yml
	EndProjectSection
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{9FD4249B-053E-4900-A0AF-834628B8B82A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{9FD4249B-053E-4900-A0AF-834628B8B82A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{9FD4249B-053E-4900-A0AF-834628B8B82A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{9FD4249B-053E-4900-A0AF-834628B8B82A}.Release|Any CPU.Build.0 = Release|Any CPU
		{2F916CD2-B6E7-44C3-8B29-5658CA166700}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{2F916CD2-B6E7-44C3-8B29-5658CA166700}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{2F916CD2-B6E7-44C3-8B29-5658CA166700}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{2F916CD2-B6E7-44C3-8B29-5658CA166700}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{50259428-AA36-4437-AF08-F1B4DFE0D580} = {F7226E79-E7AF-44D2-8071-A019146DCA06}
		{8397A6CD-B67C-4FBB-9ED9-59C92393BA16} = {50259428-AA36-4437-AF08-F1B4DFE0D580}
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

        // Copy of https://github.com/aspnet/HttpAbstractions/blob/2.2.0/HttpAbstractions.sln
        // Apache-2 licensed sln file, this string remains under that license and is only here to provide a real-world test case.
        private const string SlnContentsHttpAbstractions = @"

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.26730.10
MinimumVisualStudioVersion = 15.0.26730.03
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""src"", ""src"", ""{A5A15F1C-885A-452A-A731-B0173DDBD913}""
	ProjectSection(SolutionItems) = preProject
		src\Directory.Build.props = src\Directory.Build.props
	EndProjectSection
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""test"", ""test"", ""{F31FF137-390C-49BF-A3BD-7C6ED3597C21}""
	ProjectSection(SolutionItems) = preProject
		test\Directory.Build.props = test\Directory.Build.props
	EndProjectSection
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""samples"", ""samples"", ""{982F09D8-621E-4872-BA7B-BBDEA47D1EFD}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http"", ""src\Microsoft.AspNetCore.Http\Microsoft.AspNetCore.Http.csproj"", ""{BCF0F967-8753-4438-BD07-AADCA9CE509A}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Abstractions"", ""src\Microsoft.AspNetCore.Http.Abstractions\Microsoft.AspNetCore.Http.Abstractions.csproj"", ""{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Features"", ""src\Microsoft.AspNetCore.Http.Features\Microsoft.AspNetCore.Http.Features.csproj"", ""{D9128247-8F97-48B8-A863-F1F21A029FCE}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Tests"", ""test\Microsoft.AspNetCore.Http.Tests\Microsoft.AspNetCore.Http.Tests.csproj"", ""{AA99AF26-F7B1-4A6B-A922-5C25539F6391}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Features.Tests"", ""test\Microsoft.AspNetCore.Http.Features.Tests\Microsoft.AspNetCore.Http.Features.Tests.csproj"", ""{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Abstractions.Tests"", ""test\Microsoft.AspNetCore.Http.Abstractions.Tests\Microsoft.AspNetCore.Http.Abstractions.Tests.csproj"", ""{F16692B8-9F38-4DCA-A582-E43172B989C6}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Owin"", ""src\Microsoft.AspNetCore.Owin\Microsoft.AspNetCore.Owin.csproj"", ""{59BED991-F207-48ED-B24C-0A1D9C986C01}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Owin.Tests"", ""test\Microsoft.AspNetCore.Owin.Tests\Microsoft.AspNetCore.Owin.Tests.csproj"", ""{16219571-3268-4D12-8689-12B7163DBA13}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Extensions"", ""src\Microsoft.AspNetCore.Http.Extensions\Microsoft.AspNetCore.Http.Extensions.csproj"", ""{CCC4363E-81E2-4058-94DD-00494E9E992A}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Http.Extensions.Tests"", ""test\Microsoft.AspNetCore.Http.Extensions.Tests\Microsoft.AspNetCore.Http.Extensions.Tests.csproj"", ""{AE25EF21-7F91-4B86-B73E-AF746821D339}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.WebUtilities"", ""src\Microsoft.AspNetCore.WebUtilities\Microsoft.AspNetCore.WebUtilities.csproj"", ""{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.WebUtilities.Tests"", ""test\Microsoft.AspNetCore.WebUtilities.Tests\Microsoft.AspNetCore.WebUtilities.Tests.csproj"", ""{93C10E50-BCBB-4D8E-9492-D46E1396225B}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.Net.Http.Headers"", ""src\Microsoft.Net.Http.Headers\Microsoft.Net.Http.Headers.csproj"", ""{60AA2FDB-8121-4826-8D00-9A143FEFAF66}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.Net.Http.Headers.Tests"", ""test\Microsoft.Net.Http.Headers.Tests\Microsoft.Net.Http.Headers.Tests.csproj"", ""{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""SampleApp"", ""samples\SampleApp\SampleApp.csproj"", ""{1D0764B4-1DEB-4232-A714-D4B7E846918A}""
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""Solution Items"", ""Solution Items"", ""{C6C48D5F-B289-4150-A6FC-77A5C7064BCE}""
	ProjectSection(SolutionItems) = preProject
		.travis.yml = .travis.yml
		appveyor.yml = appveyor.yml
		build.cmd = build.cmd
		build.ps1 = build.ps1
		build.sh = build.sh
		Directory.Build.props = Directory.Build.props
		Directory.Build.targets = Directory.Build.targets
		NuGet.config = NuGet.config
		README.md = README.md
		version.xml = version.xml
	EndProjectSection
EndProject
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""build"", ""build"", ""{ED7BCAC5-2796-44BD-9954-7C248263BC8B}""
	ProjectSection(SolutionItems) = preProject
		build\dependencies.props = build\dependencies.props
		build\Key.snk = build\Key.snk
	EndProjectSection
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Authentication.Abstractions"", ""src\Microsoft.AspNetCore.Authentication.Abstractions\Microsoft.AspNetCore.Authentication.Abstractions.csproj"", ""{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Authentication.Core"", ""src\Microsoft.AspNetCore.Authentication.Core\Microsoft.AspNetCore.Authentication.Core.csproj"", ""{73CA3145-91BD-4DA5-BC74-40008DE7EA98}""
EndProject
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""Microsoft.AspNetCore.Authentication.Core.Test"", ""test\Microsoft.AspNetCore.Authentication.Core.Test\Microsoft.AspNetCore.Authentication.Core.Test.csproj"", ""{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|Mixed Platforms = Debug|Mixed Platforms
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|Mixed Platforms = Release|Mixed Platforms
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Debug|x86.ActiveCfg = Debug|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Release|Any CPU.Build.0 = Release|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{BCF0F967-8753-4438-BD07-AADCA9CE509A}.Release|x86.ActiveCfg = Release|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Debug|x86.ActiveCfg = Debug|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Release|Any CPU.Build.0 = Release|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD}.Release|x86.ActiveCfg = Release|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Debug|x86.ActiveCfg = Debug|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Release|Any CPU.Build.0 = Release|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{D9128247-8F97-48B8-A863-F1F21A029FCE}.Release|x86.ActiveCfg = Release|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Debug|x86.ActiveCfg = Debug|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Release|Any CPU.Build.0 = Release|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391}.Release|x86.ActiveCfg = Release|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Debug|x86.ActiveCfg = Debug|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Release|Any CPU.Build.0 = Release|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7}.Release|x86.ActiveCfg = Release|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Debug|x86.ActiveCfg = Debug|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Release|Any CPU.Build.0 = Release|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{F16692B8-9F38-4DCA-A582-E43172B989C6}.Release|x86.ActiveCfg = Release|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Debug|x86.ActiveCfg = Debug|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Release|Any CPU.Build.0 = Release|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{59BED991-F207-48ED-B24C-0A1D9C986C01}.Release|x86.ActiveCfg = Release|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Debug|x86.ActiveCfg = Debug|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Release|Any CPU.Build.0 = Release|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{16219571-3268-4D12-8689-12B7163DBA13}.Release|x86.ActiveCfg = Release|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Debug|x86.ActiveCfg = Debug|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Release|Any CPU.Build.0 = Release|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{CCC4363E-81E2-4058-94DD-00494E9E992A}.Release|x86.ActiveCfg = Release|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Debug|x86.ActiveCfg = Debug|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Release|Any CPU.Build.0 = Release|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{AE25EF21-7F91-4B86-B73E-AF746821D339}.Release|x86.ActiveCfg = Release|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Debug|x86.ActiveCfg = Debug|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Release|Any CPU.Build.0 = Release|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406}.Release|x86.ActiveCfg = Release|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Debug|x86.ActiveCfg = Debug|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Release|Any CPU.Build.0 = Release|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{93C10E50-BCBB-4D8E-9492-D46E1396225B}.Release|x86.ActiveCfg = Release|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Debug|x86.ActiveCfg = Debug|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Debug|x86.Build.0 = Debug|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Release|Any CPU.Build.0 = Release|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Release|x86.ActiveCfg = Release|Any CPU
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66}.Release|x86.Build.0 = Release|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Debug|x86.ActiveCfg = Debug|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Debug|x86.Build.0 = Debug|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Release|Any CPU.Build.0 = Release|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Release|x86.ActiveCfg = Release|Any CPU
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1}.Release|x86.Build.0 = Release|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Debug|x86.ActiveCfg = Debug|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Debug|x86.Build.0 = Debug|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Release|Any CPU.Build.0 = Release|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Release|x86.ActiveCfg = Release|Any CPU
		{1D0764B4-1DEB-4232-A714-D4B7E846918A}.Release|x86.Build.0 = Release|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Debug|x86.ActiveCfg = Debug|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Debug|x86.Build.0 = Debug|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Release|Any CPU.Build.0 = Release|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Release|x86.ActiveCfg = Release|Any CPU
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852}.Release|x86.Build.0 = Release|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Debug|x86.ActiveCfg = Debug|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Debug|x86.Build.0 = Debug|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Release|Any CPU.Build.0 = Release|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Release|x86.ActiveCfg = Release|Any CPU
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98}.Release|x86.Build.0 = Release|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Debug|x86.ActiveCfg = Debug|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Debug|x86.Build.0 = Debug|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Release|Any CPU.Build.0 = Release|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Release|x86.ActiveCfg = Release|Any CPU
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7}.Release|x86.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{BCF0F967-8753-4438-BD07-AADCA9CE509A} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{22071333-15BA-4D16-A1D5-4D5B1A83FBDD} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{D9128247-8F97-48B8-A863-F1F21A029FCE} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{AA99AF26-F7B1-4A6B-A922-5C25539F6391} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{C5D2BAE1-E182-48A0-AA74-1AF14B782BF7} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{F16692B8-9F38-4DCA-A582-E43172B989C6} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{59BED991-F207-48ED-B24C-0A1D9C986C01} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{16219571-3268-4D12-8689-12B7163DBA13} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{CCC4363E-81E2-4058-94DD-00494E9E992A} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{AE25EF21-7F91-4B86-B73E-AF746821D339} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{A2FB7838-0031-4FAD-BA3E-83C30B3AF406} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{93C10E50-BCBB-4D8E-9492-D46E1396225B} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{60AA2FDB-8121-4826-8D00-9A143FEFAF66} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{E6BB7AD1-BD10-4A23-B780-F4A86ADF00D1} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
		{1D0764B4-1DEB-4232-A714-D4B7E846918A} = {982F09D8-621E-4872-BA7B-BBDEA47D1EFD}
		{ED7BCAC5-2796-44BD-9954-7C248263BC8B} = {C6C48D5F-B289-4150-A6FC-77A5C7064BCE}
		{3D8C9A87-5DFB-4EC0-9CB6-174AD3B33852} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{73CA3145-91BD-4DA5-BC74-40008DE7EA98} = {A5A15F1C-885A-452A-A731-B0173DDBD913}
		{A85950C5-2794-47E2-8EAA-05A1DC7C6DA7} = {F31FF137-390C-49BF-A3BD-7C6ED3597C21}
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {D9A9994D-F09F-4209-861B-4A9036485D1F}
	EndGlobalSection
EndGlobal";

        [Theory]
        [InlineData(nameof(SlnContentsSlnParser), SlnContentsSlnParser)]
        [InlineData(nameof(SlnContentsTestSln), SlnContentsTestSln)]
        [InlineData(nameof(SlnContentsDotnetNew), SlnContentsDotnetNew)]
        [InlineData(nameof(SlnContentsHideSolutionNode), SlnContentsHideSolutionNode)]
        [InlineData(nameof(SlnContentsNoProperties), SlnContentsNoProperties)]
        [InlineData(nameof(SlnContentsSlnSync), SlnContentsSlnSync)]
        [InlineData(nameof(SlnContentsHttpAbstractions), SlnContentsHttpAbstractions)]
        public void Should_RoundTripFile(string name, string originalSln)
        {
            // Arrange
            var solution = new Solution(originalSln);

            // Act
            var output = solution.ToString();

            // Write to files for easier debugging,
            // e.g. `dotnet test; kdiff3 ./Tests/bin/Debug/net8.0/SlnContentsHttpAbstractions-{input,output}.sln &`
            // Note JetBrains Rider test runner doesn't write the files in the above folder, use the CLI to generate files.
            File.WriteAllText($"{name}-input.sln", originalSln);
            File.WriteAllText($"{name}-output.sln", output);

            // Assert
            output.Trim().Should().Be(originalSln.Trim());
        }

        [Fact]
        public void Should_ModifyFile()
        {
            // Arrange
            var solution = new Solution(SlnContentsSlnParser);

            // Act
            solution.Projects.Add(
                new SolutionFolder(Guid.NewGuid(), name: "foo-project", path: "foo/"));

            // Assert
            solution.ToString().Should().Contain("foo-project");
        }
    }
}
