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

        [Fact]
        [Category("ParseSolution:SlnParser")]
        public void Should_RoundTripFile()
        {
            const string solutionFileName = $"./Solutions/SlnParser.sln";
            var solutionFile = LoadSolution(solutionFileName);
            var original = File.ReadAllText(solutionFileName);
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
