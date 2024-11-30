using Microsoft.Extensions.Logging;

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

            SolvePuzzleTwo("./data/day2/test2.txt");

            SolvePuzzleTwo("./data/day2/input1.txt");
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
