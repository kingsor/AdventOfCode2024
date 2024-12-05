using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConsoleMainApp.TaskRunners
{
    public class PuzzleSolverDay2 : IPuzzleSolver
    {
        private readonly ILogger<PuzzleSolverDay2> _logger;

        public PuzzleSolverDay2(ILogger<PuzzleSolverDay2> logger)
        {
            _logger = logger;
        }

        public void Run(bool solveFirst = true)
        {
            if (solveFirst)
            {
                SolvePuzzleOne("./data/day2/test1.txt");

                SolvePuzzleOne("./data/day2/input1.txt");
            }

            SolvePuzzleTwo("./data/day2/test1.txt");

            SolvePuzzleTwo("./data/day2/input1.txt");
        }

        private void SolvePuzzleOne(string inputFile)
        {
            _logger.LogInformation("Solving puzzle one");

            _logger.LogInformation($"Input file: {inputFile}");

            if (File.Exists(inputFile))
            {
                var lines = File.ReadAllLines(inputFile);
                var reportList = new List<int>();
                var safeNum = 0;

                foreach (var line in lines)
                {
                    var nums = line.Split(" ").ToList();

                    nums.ForEach(n => reportList.Add(Convert.ToInt32(n)));

                    safeNum += IsReportSafe(reportList) ? 1 : 0;

                    reportList.Clear();
                }

                _logger.LogInformation($"Safe reports: {safeNum}");
            }
            else
            {
                _logger.LogError("File not found");
            }
        }

        private bool IsReportSafe(List<int> report)
        {
            bool allIncreasing = report.Zip(report.Skip(1), (a, b) =>
            {
                var res = b - a is >= 1 and <= 3;
                return res;
            }).All(b => b);


            bool allDecreasing = report.Zip(report.Skip(1), (a, b) =>
            {
                var res = a - b is >= 1 and <= 3;
                return res;
            }).All(b => b);

            return allIncreasing || allDecreasing;
        }

        private void SolvePuzzleTwo(string inputFile)
        {
            _logger.LogInformation("Solving puzzle two");

            _logger.LogInformation($"Input file: {inputFile}");

            if (File.Exists(inputFile))
            {
                var lines = File.ReadAllLines(inputFile);
                var reportList = new List<int>();
                var safeNum = 0;

                foreach (var line in lines)
                {
                    var nums = line.Split(" ").ToList();

                    nums.ForEach(n => reportList.Add(Convert.ToInt32(n)));

                    safeNum += IsReportSafe2(reportList) ? 1 : 0;

                    reportList.Clear();
                }

                _logger.LogInformation($"Safe reports: {safeNum}");
            }
            else
            {
                _logger.LogError("File not found");
            }
        }

        private bool IsReportSafe2(List<int> report)
        {
            for (var i = 0; i < report.Count; i++)
            {
                if (IsReportSafe([.. report[..i], .. report[(i + 1)..]]))
                    return true;
            }

            return false;
        }
    }
}
