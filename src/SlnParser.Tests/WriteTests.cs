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
        [InlineData("./Solutions/SlnParser.sln")]
        [InlineData("./Solutions/sln-items-sync.sln")]
        public void Should_RoundTripFile(string solutionPath)
        {
            var solutionFile = LoadSolution(solutionPath);
            var original = File.ReadAllText(solutionPath);
            var sut = new SolutionParser();

            var solution = sut.Parse(solutionFile);

            var actual = solution.Write();
            actual.Should().Be(original);
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
