using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay03 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay03> _logger;

    public PuzzleSolverDay03(ILogger<PuzzleSolverDay03> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        SolvePuzzleOne("./data/day03/test1.txt");

        SolvePuzzleOne("./data/day03/input1.txt");

        SolvePuzzleTwo("./data/day03/test2.txt");

        SolvePuzzleTwo("./data/day03/input1.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var input = File.ReadAllText(inputFile);

            var expr = @"mul\((\d+),(\d+)\)";

            MatchCollection mc = Regex.Matches(input, expr);

            var result = mc.Select(m => long.Parse(m.Groups[1].ValueSpan) * long.Parse(m.Groups[2].ValueSpan))
                           .Sum()
                           .ToString();

            _logger.LogInformation($"Result: {result}");
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
            var input = File.ReadAllText(inputFile);

            var expr = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";

            MatchCollection mc = Regex.Matches(input, expr);

            _logger.LogInformation($"MatchCollection count: {mc.Count}");

            var sum = 0L;
            var state = true;

            foreach (Match m in mc)
            {
                var name = m.Groups[0].Value;

                if (state)
                {
                    if (name.StartsWith("mul"))
                    {
                        sum += long.Parse(m.Groups[1].ValueSpan) * long.Parse(m.Groups[2].ValueSpan);
                    }

                    if (name.StartsWith("don"))
                    {
                        state = false;
                    }
                }
                else
                {
                    if (name.StartsWith("do("))
                    {
                        state = true;
                    }
                }
            }

            var result = sum.ToString();
            _logger.LogInformation($"Result: {result}");

        }
        else
        {
            _logger.LogError("File not found");
        }
    }
}
