using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners
{
    public class PuzzleSolverDay6 : IPuzzleSolver
    {

        private readonly ILogger<PuzzleSolverDay6> _logger;

        public PuzzleSolverDay6(ILogger<PuzzleSolverDay6> logger)
        {
            _logger = logger;
        }

        public void Run(bool solveFirst = true)
        {
            if (solveFirst)
            {
                SolvePuzzleOne("./data/day6/test1.txt");

                SolvePuzzleOne("./data/day6/input1.txt");
            }

            SolvePuzzleTwo("./data/day6/test2.txt");

            SolvePuzzleTwo("./data/day6/input2.txt");
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
}
