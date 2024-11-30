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

        SolvePuzzleTwo("./data/day1/test2.txt");

        SolvePuzzleTwo("./data/day1/input1.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            // Solve puzzle
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
            // Solve Puzzle
        }
        else
        {
            _logger.LogError("File not found");
        }
    }
}
