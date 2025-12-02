using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMainApp.TaskRunners
{
    internal class PuzzleSolverDay12 : IPuzzleSolver
    {
        private readonly ILogger<PuzzleSolverDay12> _logger;

        public PuzzleSolverDay12(ILogger<PuzzleSolverDay12> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            SolvePuzzles("./data/day12/test1.txt");

            SolvePuzzles("./data/day12/input1.txt");
        }

        private void SolvePuzzles(string inputFile)
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
    }
}
