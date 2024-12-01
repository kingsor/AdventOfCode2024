using ConsoleMainApp.Extensions;
using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay1 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay1> _logger;

    public PuzzleSolverDay1(ILogger<PuzzleSolverDay1> logger)
    {
        _logger = logger;
    }

    public void Run(bool solveFirst = true)
    {
        if (solveFirst)
        {
            SolvePuzzleOne("./data/day1/test1.txt");

            SolvePuzzleOne("./data/day1/input1.txt");
        }

        SolvePuzzleTwo("./data/day1/test1.txt");

        SolvePuzzleTwo("./data/day1/input1.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var lines = File.ReadAllLines(inputFile);
            var leftList = new List<int>();
            var rightList = new List<int>();
            var totDistance = 0;

            foreach (var line in lines)
            {
                var nums = line.Split("   ");

                leftList.Add(Convert.ToInt32(nums[0]));
                rightList.Add(Convert.ToInt32(nums[1]));
            }

            var listLen = leftList.Count;

            for (int idx = 0; idx < listLen; idx++)
            {
                var leftMin = leftList.Min();
                var rightMin = rightList.Min();
                var distance = Math.Abs(leftMin - rightMin);

                _logger.LogInformation($"Current distance [{idx}]: {distance}");

                totDistance += distance;
                leftList.Remove(leftMin);
                rightList.Remove(rightMin);
            }

            _logger.LogInformation($"Total distance: {totDistance}");
        }
        else
        {
            _logger.LogError("File not found");
        }
    }

    private void SolvePuzzleTwo(string inputFile)
    {
        _logger.LogInformation("Solving puzzle two");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var lines = File.ReadAllLines(inputFile);
            var leftList = new List<int>();
            var rightList = new List<int>();
            var totScore = 0;

            foreach (var line in lines)
            {
                var nums = line.Split("   ");

                leftList.Add(Convert.ToInt32(nums[0]));
                rightList.Add(Convert.ToInt32(nums[1]));
            }

            foreach(var num in leftList)
            {
                var numTimes = rightList.Count(r => r == num);

                var score = num * numTimes;

                _logger.LogInformation($"Similarity score for [{num}]: {score}");

                totScore += score;
            }

            _logger.LogInformation($"Total similarity score: {totScore}");
        }
        else
        {
            _logger.LogError("File not found");
        }
    }
}
