using FluentAssertions;
using SlnEditor.Mappings;
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
# Visual Studio Version 15
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
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
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

        [Theory]
        [InlineData(SlnContentsSlnParser)]
        [InlineData(SlnContentsTestSln)]
        [InlineData(SlnContentsDotnetNew)]
        [InlineData(SlnContentsSlnSync)]
        public void Should_RoundTripFile(string originalSln)
        {
            var solution = new Solution(originalSln);
            var output = solution.Write();
            output.Trim().Should().Be(originalSln.Trim());
        }

        [Fact]
        public void Should_ModifyFile()
        {
            // Arrange
            var solution = new Solution(SlnContentsSlnParser);

            // Act
            solution.Projects.Add(
                new SolutionFolder(Guid.NewGuid(), name: "foo-project", path: "foo/", ProjectType.Test));

            // Assert
            solution.Write().Should().Contain("foo-project");
        }

        [Fact]
        public void Should_CreateFile()
        {
            const string solutionName = "NewSolution.sln";

            var solution = new Solution();

            solution.Projects.Add(new SolutionFolder(
                Guid.NewGuid(),
                name: "foo",
                path: "foo/",
                typeGuid: new ProjectTypeMap().Guids[ProjectType.Test],
                ProjectType.Test));

            var actual = solution.Write();
            Directory.CreateDirectory($"./newsln/");
            File.WriteAllText($"./newsln/{solutionName}", actual);
        }
    }
}
