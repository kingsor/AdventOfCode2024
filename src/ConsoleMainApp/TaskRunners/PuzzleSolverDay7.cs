using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMainApp.TaskRunners
{
    internal class PuzzleSolverDay7 : IPuzzleSolver
    {
        private readonly ILogger<PuzzleSolverDay7> _logger;

        public PuzzleSolverDay7(ILogger<PuzzleSolverDay7> logger)
        {
            _logger = logger;
        }

        public void Run(bool solveFirst = true)
        {
            if (solveFirst)
            {
                SolvePuzzle("./data/day7/test1.txt");

                SolvePuzzle("./data/day7/input1.txt");
            }
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
