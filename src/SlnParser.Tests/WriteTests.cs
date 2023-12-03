using FluentAssertions;
using SlnParser.Contracts;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Xunit;

namespace SlnParser.Tests
{
    public class WriteTests
    {

        [Theory]
        [InlineData("SlnParser.sln")]
        [InlineData("sln-items-sync.sln")]
        public void Should_RoundTripFile(string solutionName)
        {
            var solutionPath = $"./Solutions/{solutionName}";
            var solutionFile = LoadSolution(solutionPath);
            var original = File.ReadAllText(solutionPath);
            var sut = new SolutionParser();

            var solution = sut.Parse(solutionFile);

            var actual = solution.Write();
            Directory.CreateDirectory($"./roundtrip/");
            File.WriteAllText($"./roundtrip/{solutionName}", actual);
            actual.Trim().Should().Be(original.Trim());
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
