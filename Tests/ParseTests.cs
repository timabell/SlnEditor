using FluentAssertions;
using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SlnEditor.Tests
{
    public class ParseTests
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
Project(""{00D1A9C2-B5F0-4AF3-8072-F6C62B433612}"") = ""SSDT Database Project"", ""Database\Database.sqlproj"", ""{400900EF-5FB6-4F11-AC39-384F4F5D3E64}""
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

        private const string SlnContentsProjectWithoutPlatform = @"
Microsoft Visual Studio Solution File, Format Version 10.00
# Visual Studio 2008
Project(""{D183A3D8-5FD8-494B-B014-37F57B35E655}"") = ""Test"", ""Test.dtproj"", ""{D5BDBC46-CEAF-4C92-8335-31450B76914F}""
EndProject
Global
        GlobalSection(SolutionConfigurationPlatforms) = preSolution
                SolutionConfigurationName|SolutionPlatformName = SolutionConfigurationName|SolutionPlatformName
        EndGlobalSection
        GlobalSection(ProjectConfigurationPlatforms) = postSolution
                {D5BDBC46-CEAF-4C92-8335-31450B76914F}.SolutionConfigurationName|SolutionPlatformName.ActiveCfg = ProjectConfigurationName
                {D5BDBC46-CEAF-4C92-8335-31450B76914F}.SolutionConfigurationName|SolutionPlatformName.Build.0 = ProjectConfigurationName
        EndGlobalSection
EndGlobal
";

        private const string SlnContentsSolutionGuid = @"
Global
        GlobalSection(ExtensibilityGlobals) = postSolution 
            SolutionGuid = {7F92F20E-4C3D-4316-BF60-105559EFEAFF} 
        EndGlobalSection 
EndGlobal
";

        private const string SlnContentsUnknownConfigGuid = @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31410.414
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"") = ""SlnParser"", ""SlnParser\SlnParser.csproj"", ""{DFB5E15D-81A7-4BA9-8A39-98D9D5E38297}""
EndProject
Global
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{DFB5E15D-81A7-4BA9-8A39-98D9D5E38297}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{DFB5E15D-81A7-4BA9-8A39-98D9D5E38297}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\r\n")]
        [InlineData("\n")]
        public void Parse_WithEmptySolutionFile_IsParsedCorrectly(string slnContents)
        {
            var solution = new Solution(slnContents);

            solution
                .FileFormatVersion
                .Should()
                .Be(string.Empty);

            var visualStudioVersion = solution.VisualStudioVersion;

            visualStudioVersion
                .MinimumVersion
                .Should()
                .Be(string.Empty);

            visualStudioVersion
                .Version
                .Should()
                .Be(string.Empty);

            solution
                .Guid
                .Should()
                .Be(null);

            solution
                .ConfigurationPlatforms
                .Should()
                .HaveCount(0);

            solution
                .RootProjects
                .Should()
                .HaveCount(0);

            solution
                .RootProjects
                .Should()
                .HaveCount(0);
        }

        [Fact]
        public void Should_Be_Able_To_Parse_SlnParser_Solution_Correctly()
        {
            var solution = new Solution(SlnContentsSlnParser);

            solution
                .FileFormatVersion
                .Should()
                .Be("12.00");

            solution
                .VisualStudioVersion
                .Version
                .Should()
                .Be("17.0.31410.414");

            solution
                .VisualStudioVersion
                .MinimumVersion
                .Should()
                .Be("10.0.40219.1");

            // -- Solution Configuration Platforms

            solution
                .ConfigurationPlatforms
                .Should()
                .HaveCount(6);

            solution
                .ConfigurationPlatforms
                .ElementAt(0)
                .Name
                .Should()
                .Be("Debug|Any CPU");

            solution
                .ConfigurationPlatforms
                .ElementAt(0)
                .Configuration
                .Should()
                .Be("Debug");

            solution
                .ConfigurationPlatforms
                .ElementAt(0)
                .Platform
                .Should()
                .Be("Any CPU");

            solution
                .ConfigurationPlatforms
                .ElementAt(1)
                .Name
                .Should()
                .Be("Debug|x64");

            solution
                .ConfigurationPlatforms
                .ElementAt(1)
                .Configuration
                .Should()
                .Be("Debug");

            solution
                .ConfigurationPlatforms
                .ElementAt(1)
                .Platform
                .Should()
                .Be("x64");

            solution
                .ConfigurationPlatforms
                .ElementAt(2)
                .Name
                .Should()
                .Be("Debug|x86");

            solution
                .ConfigurationPlatforms
                .ElementAt(2)
                .Configuration
                .Should()
                .Be("Debug");

            solution
                .ConfigurationPlatforms
                .ElementAt(2)
                .Platform
                .Should()
                .Be("x86");

            solution
                .ConfigurationPlatforms
                .ElementAt(3)
                .Name
                .Should()
                .Be("Release|Any CPU");

            solution
                .ConfigurationPlatforms
                .ElementAt(3)
                .Configuration
                .Should()
                .Be("Release");

            solution
                .ConfigurationPlatforms
                .ElementAt(3)
                .Platform
                .Should()
                .Be("Any CPU");

            solution
                .ConfigurationPlatforms
                .ElementAt(4)
                .Name
                .Should()
                .Be("Release|x64");

            solution
                .ConfigurationPlatforms
                .ElementAt(4)
                .Configuration
                .Should()
                .Be("Release");

            solution
                .ConfigurationPlatforms
                .ElementAt(4)
                .Platform
                .Should()
                .Be("x64");

            solution
                .ConfigurationPlatforms
                .ElementAt(5)
                .Name
                .Should()
                .Be("Release|x86");

            solution
                .ConfigurationPlatforms
                .ElementAt(5)
                .Configuration
                .Should()
                .Be("Release");

            solution
                .ConfigurationPlatforms
                .ElementAt(5)
                .Platform
                .Should()
                .Be("x86");

            // -- Projects
            solution
                .RootProjects
                .Should()
                .HaveCount(3);

            // 1. Project - ClassLib
            solution
                .RootProjects.First().Should().BeOfType<Project>()
                .Which.Should().BeEquivalentTo(new
                {
                    Name = "SlnParser",
                    Type = ProjectType.CSharp,
                    SourceLine = 6,
                });

            solution
                .RootProjects
                .ElementAt(0)
                .As<Project>()
                .ConfigurationPlatforms
                .Should()
                .Contain(config => config.Name.Equals("Debug|Any CPU.ActiveCfg"));

            // 2. Project - Solution Folder
            solution
                .RootProjects.Skip(1).First().Should().BeOfType<SolutionFolder>()
                .Which.Should().BeEquivalentTo(new
                {
                    Name = "Solution Items",
                    Type = ProjectType.SolutionFolder,
                    SourceLine = 8,
                    Projects = new List<IProject>(),
                });

            // 3. Project - Test Project
            solution
                .RootProjects
                .ElementAt(2)
                .Should()
                .BeOfType<Project>();
            solution
                .RootProjects
                .ElementAt(2)
                .Name
                .Should()
                .Be("SlnParser.Tests");
            solution
                .RootProjects
                .ElementAt(2)
                .Type
                .Should()
                .Be(ProjectType.CSharp2);

            solution
                .RootProjects
                .ElementAt(2)
                .As<Project>()
                .ConfigurationPlatforms
                .Should()
                .Contain(config => config.Name.Equals("Debug|x86.Build.0"));
        }

        [Fact]
        public void Should_Be_Able_To_Parse_TestSln_Solution_Correctly()
        {
            var solution = new Solution(SlnContentsTestSln);

            solution
                .FlatProjectList()
                .Should()
                .HaveCount(8);

            solution
                .RootProjects
                .Should()
                .HaveCount(4);

            var firstSolutionFolder = solution
                .RootProjects
                .OfType<SolutionFolder>()
                .FirstOrDefault(folder => folder.Name == "SolutionFolder1");

            Assert.NotNull(firstSolutionFolder);

            firstSolutionFolder
                .Files
                .Should()
                .Contain(file => file == "something.txt" ||
                                 file == "test123.txt" ||
                                 file == "test456.txt");

            var nestedSolutionFolder = solution
                .FlatProjectList()
                .OfType<SolutionFolder>()
                .FirstOrDefault(folder => folder.Name == "NestedSolutionFolder");

            Assert.NotNull(nestedSolutionFolder);

            nestedSolutionFolder
                .Files
                .Should()
                .Contain(file => file == "testNested1.txt");

            solution.GlobalSection<NestedProjectsSection>().SourceLine.Should().Be(113);
            solution.GlobalSection<SolutionPropertiesSection>().SourceLine.Should().Be(119);
        }

        [Fact]
        public void Parse_WithProjectWithoutPlatform_IsParsedCorrectly()
        {
            var solution = new Solution(SlnContentsProjectWithoutPlatform);

            solution
                .ConfigurationPlatforms
                .Should()
                .HaveCount(1);

            var configurationPlatform = solution
                .ConfigurationPlatforms
                .Single();

            configurationPlatform
                .Configuration
                .Should()
                .Be("SolutionConfigurationName");

            configurationPlatform
                .Platform
                .Should()
                .Be("SolutionPlatformName");

            solution
                .RootProjects
                .Should()
                .HaveCount(1);

            solution
                .RootProjects
                .Should()
                .HaveCount(1);

            var project = solution.RootProjects.Single();
            project.Id.Should().Be("D5BDBC46-CEAF-4C92-8335-31450B76914F");
            project.Name.Should().Be("Test");
            project.TypeGuid.Should().Be("D183A3D8-5FD8-494B-B014-37F57B35E655");
            project.Type.Should().Be(ProjectType.SSIS);

            solution.GlobalSection<ConfigurationPlatformsSection>().SourceLine.Should().Be(7);
            solution.GlobalSection<ProjectConfigurationPlatformsSection>().SourceLine.Should().Be(10);
        }

        [Fact]
        public void Parse_WithSolutionGuid_IsParsedCorrectly()
        {
            var solution = new Solution(SlnContentsSolutionGuid);

            solution
                .Guid
                .Should()
                .Be("7F92F20E-4C3D-4316-BF60-105559EFEAFF");

            solution.GlobalSection<ExtensibilityGlobalsSection>().SourceLine.Should().Be(3);
        }

        [Fact]
        public void Parse_WithUnexpectedConfig_IsParsedCorrectly()
        {
            var solution = new Solution(SlnContentsUnknownConfigGuid);

            solution.RootProjects.OfType<Project>().SelectMany(p => p.ConfigurationPlatforms).Count()
                .Should().Be(2, because: "two out of the three configs have valid project guids");
        }
    }
}
