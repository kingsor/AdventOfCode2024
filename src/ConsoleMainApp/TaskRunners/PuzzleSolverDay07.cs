using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners
{
    internal class PuzzleSolverDay07 : IPuzzleSolver
    {
        private readonly ILogger<PuzzleSolverDay07> _logger;

        public PuzzleSolverDay07(ILogger<PuzzleSolverDay07> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            SolvePuzzle("./data/day07/test1.txt");

            SolvePuzzle("./data/day07/input1.txt");
        }

        internal class PuzzleItem
        {
            public long TestValue { get; set; }
            public List<long> Numbers { get; set; }
        }

        private void SolvePuzzle(string inputFile)
        {
            _logger.LogInformation("Solving puzzle one");

            _logger.LogInformation($"Input file: {inputFile}");

            if (File.Exists(inputFile))
            {
                var lines = File.ReadAllLines(inputFile);

                var puzzles = new List<PuzzleItem>();

                foreach (var line in lines)
                {
                    var res = line.Split(": ");

                    var item = new PuzzleItem
                    {
                        TestValue = long.Parse(res[0]),
                        Numbers = res[1].Split(" ").Select(long.Parse).ToList()
                    };

                    puzzles.Add(item);
                }

                var result1 = 0L;
                var result2 = 0L;

                foreach (var item in puzzles)
                {
                    if (IsValid(item.TestValue, item.Numbers))
                    {
                        result1 += item.TestValue;
                    }

                    if (IsValid(item.TestValue, item.Numbers, true))
                    {
                        result2 += item.TestValue;
                    }
                }

                _logger.LogInformation($"Result1: {result1}");

                _logger.LogInformation($"Result2: {result2}");

            }
            else
            {
                _logger.LogError("File not found");
            }
        }

        private bool IsValid(long testValue, List<long> numbers, bool isPuzzleTwo = false)
        {
            if (numbers.Count == 1)
                return numbers[0] == testValue;


            if (IsValid(testValue, [numbers[0] + numbers[1], .. numbers[2..]], isPuzzleTwo))
            {
                return true;
            }

            if (IsValid(testValue, [numbers[0] * numbers[1], .. numbers[2..]], isPuzzleTwo))
            {
                return true;
            }

            if (isPuzzleTwo)
            {
                var concat = long.Parse($"{numbers[0]}{numbers[1]}");
                if (IsValid(testValue, [concat, .. numbers[2..]], isPuzzleTwo))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}
