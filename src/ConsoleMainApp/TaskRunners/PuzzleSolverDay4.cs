using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay4 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay4> _logger;

    public PuzzleSolverDay4(ILogger<PuzzleSolverDay4> logger)
    {
        _logger = logger;
    }

    public void Run(bool solveFirst = true)
    {
        if (solveFirst)
        {
            SolvePuzzleOne("./data/day4/test1.txt");

            SolvePuzzleOne("./data/day4/input1.txt");
        }

        SolvePuzzleTwo("./data/day4/test2.txt");

        SolvePuzzleTwo("./data/day4/input2.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            // solve puzzle
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
            // solve puzzle
        }
        else
        {
            _logger.LogError("File not found");
        }
    }
}
