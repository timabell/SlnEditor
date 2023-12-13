using FluentAssertions;
using SlnEditor;
using SlnEditor.Contracts;
using SlnEditor.Helper;
using System;
using System.IO;
using Xunit;

namespace SlnParser.Tests
{
    public class WriteTests
    {
        [Theory]
        [InlineData("SlnParser.sln")]
        [InlineData("TestSln.sln")]
        [InlineData("DotnetNew.sln")]
        [InlineData("sln-items-sync.sln")]
        public void Should_RoundTripFile(string sourceSolutionName)
        {
            var sourceSolutionPath = $"./Solutions/{sourceSolutionName}";
            var sourceSolutionFile = LoadSolution(sourceSolutionPath);
            var original = File.ReadAllText(sourceSolutionPath);
            var solutionParser = new SolutionParser();

            var solution = solutionParser.Parse(sourceSolutionFile);

            var actual = solution.Write();
            Directory.CreateDirectory($"./roundtrip/");
            File.WriteAllText($"./roundtrip/{sourceSolutionName}", actual);
            actual.Trim().Should().Be(original.Trim());
        }

        [Fact]
        public void Should_ModifyFile()
        {
            const string sourceSolutionName = "SlnParser.sln";
            var sourceSolutionPath = $"./Solutions/{sourceSolutionName}";
            var sourceSolutionFile = LoadSolution(sourceSolutionPath);
            var solutionParser = new SolutionParser();

            var solution = solutionParser.Parse(sourceSolutionFile);

            var mapper = new ProjectTypeMapper();
            solution.Projects.Add(
                new SolutionFolder(Guid.NewGuid(), name: "foo-project", path: "foo/", typeGuid: mapper.ToGuid(ProjectType.Test), ProjectType.Test));
            var actual = solution.Write();
            Directory.CreateDirectory($"./modified/");
            File.WriteAllText($"./modified/{sourceSolutionName}", actual);
            actual.Should().Contain("foo-project");
        }

        [Fact]
        public void Should_CreateFile()
        {
            const string solutionName = "NewSolution.sln";

            var solution = new Solution();

            var mapper = new ProjectTypeMapper();
            solution.Projects.Add(
                new SolutionFolder(Guid.NewGuid(), name: "foo", path: "foo/", typeGuid: mapper.ToGuid(ProjectType.Test), ProjectType.Test));
            var actual = solution.Write();
            Directory.CreateDirectory($"./newsln/");
            File.WriteAllText($"./newsln/{solutionName}", actual);
        }

        private static FileInfo LoadSolution(string solutionFileName)
        {
            var solutionFile = new FileInfo(solutionFileName);

            if (!solutionFile.Exists)
                throw new FileNotFoundException();

            return solutionFile;
        }
    }
}
