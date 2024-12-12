using ConsoleMainApp.Helpers;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay04 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay04> _logger;

    public PuzzleSolverDay04(ILogger<PuzzleSolverDay04> logger)
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

        SolvePuzzleTwo("./data/day4/test1.txt");

        SolvePuzzleTwo("./data/day4/input1.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var map = FileHelper.GetBytesMap(inputFile);

            var count = 0;

            List<(int x, int y)> adjacent = [
                (-1, -1),
                (-1, 0),
                (-1, 1),
                (0, 1),
                (0, -1),
                (1, 1),
                (1, 0),
                (1, -1),
                ];

            for (var y = 0; y < map.Length; y++)
            {
                for (var x = 0; x < map[y].Length; x++)
                {
                    _logger.LogInformation($"(y, x) = ({y}, {x})");

                    if (map[y][x] is not (byte)'X')
                        continue;

                    _logger.LogInformation("> X");

                    foreach (var (dx, dy) in adjacent)
                    {
                        var (nx, ny) = (x + dx, y + dy);

                        _logger.LogInformation($"(nx, ny) = ({nx}, {ny})");

                        if (nx < 0 || ny < 0 || nx >= map[y].Length || ny >= map.Length)
                            continue;
                        if (map[ny][nx] != (byte)'M')
                            continue;

                        _logger.LogInformation("> M");

                        (nx, ny) = (nx + dx, ny + dy);

                        _logger.LogInformation($"(nx, ny) = ({nx}, {ny})");

                        if (nx < 0 || ny < 0 || nx >= map[y].Length || ny >= map.Length)
                            continue;
                        if (map[ny][nx] != (byte)'A')
                            continue;

                        _logger.LogInformation("> A");

                        (nx, ny) = (nx + dx, ny + dy);

                        _logger.LogInformation($"(nx, ny) = ({nx}, {ny})");

                        if (nx < 0 || ny < 0 || nx >= map[y].Length || ny >= map.Length)
                            continue;
                        if (map[ny][nx] != (byte)'S')
                            continue;

                        _logger.LogInformation("> S");

                        count++;

                        _logger.LogInformation($"count: {count}");
                    }
                }
            }

            _logger.LogInformation($"Result: {count}");

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
            var map = FileHelper.GetBytesMap(inputFile);

            var count = 0;

            for (var y = 0; y < map.Length; y++)
            {
                for (var x = 0; x < map[y].Length; x++)
                {
                    _logger.LogInformation($"(y, x) = ({y}, {x})");

                    if (x == 0 || y == 0 || x == map[y].Length - 1 || y == map.Length - 1)
                    {
                        _logger.LogInformation("> B");
                        continue;
                    }

                    if (map[y][x] != (byte)'A')
                        continue;

                    _logger.LogInformation("> A");

                    if (
                        (map[y - 1][x - 1], map[y + 1][x + 1]) is ((byte)'M', (byte)'S') or ((byte)'S', (byte)'M')
                        && (map[y - 1][x + 1], map[y + 1][x - 1]) is ((byte)'M', (byte)'S') or ((byte)'S', (byte)'M')
                    )
                    {
                        count++;
                        _logger.LogInformation($"count: {count}");
                    }
                }
            }

            //LogMap(map);

            _logger.LogInformation($"Result: {count}");
        }
        else
        {
            _logger.LogError("File not found");
        }
    }

    private void LogMap(byte[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            var line = new string(Encoding.ASCII.GetChars(map[y]));
            _logger.LogInformation($"{line}");
        }
    }
}
