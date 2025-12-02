using ConsoleMainApp.Helpers;
using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners;

static class Direction
{
    public const byte Up = (byte)'^';
    public const byte Down = (byte)'v';
    public const byte Left = (byte)'<';
    public const byte Right = (byte)'>';
    public const byte Step = (byte)'X';
    public const byte Obstacle = (byte)'#';
}

public class PuzzleSolverDay06 : IPuzzleSolver
{

    private readonly ILogger<PuzzleSolverDay06> _logger;

    public PuzzleSolverDay06(ILogger<PuzzleSolverDay06> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        SolvePuzzleOne("./data/day06/test1.txt");

        SolvePuzzleOne("./data/day06/input1.txt");

        SolvePuzzleTwo("./data/day06/test2.txt");

        SolvePuzzleTwo("./data/day06/input2.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var map = FileHelper.GetBytesMap(inputFile);

            // find starting point and direction

            (int, int) curPos = (0, 0);

            var direction = Direction.Down;

            var found = false;

            for (var y = 0; y < map.Length; y++)
            {
                if (found)
                {
                    break;
                }

                for (var x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] is Direction.Up or Direction.Left or Direction.Right or Direction.Down)
                    {
                        curPos = (y, x);
                        direction = map[y][x];
                        found = true;
                        break;
                    }
                }
            }

            _logger.LogInformation($"curPos = {curPos} - direction = {(char)direction}");

            // start moving and mark each move with X

            var canMove = true;

            int newY = 0;
            int newX = 0;

            while (canMove)
            {
                switch (direction)
                {
                    case Direction.Up:
                        newY = curPos.Item1 - 1;
                        if (newY < 0)
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            canMove = false;
                        }
                        else if (map[newY][curPos.Item2] is Direction.Obstacle)
                        {
                            direction = Direction.Right;
                        }
                        else
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            curPos.Item1 = newY;
                        }
                        break;
                    case Direction.Down:
                        newY = curPos.Item1 + 1;
                        if (newY > map.Length - 1)
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            canMove = false;
                        }
                        else if (map[newY][curPos.Item2] is Direction.Obstacle)
                        {
                            direction = Direction.Left;
                        }
                        else
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            curPos.Item1 = newY;
                        }
                        break;
                    case Direction.Left:
                        newX = curPos.Item2 - 1;
                        if (newX < 0)
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            canMove = false;
                        }
                        else if (map[curPos.Item1][newX] is Direction.Obstacle)
                        {
                            direction = Direction.Up;
                        }
                        else
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            curPos.Item2 = newX;
                        }
                        break;
                    case Direction.Right:
                        newX = curPos.Item2 + 1;
                        if (newX > map[curPos.Item1].Length - 1)
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            canMove = false;
                        }
                        else if (map[curPos.Item1][newX] is Direction.Obstacle)
                        {
                            direction = Direction.Down;
                        }
                        else
                        {
                            map[curPos.Item1][curPos.Item2] = Direction.Step;
                            curPos.Item2 = newX;
                        }
                        break;
                }

                _logger.LogInformation($"curPos = {curPos} - direction = {(char)direction}");
            }

            // result = count X in map
            var count = 0;

            for (var y = 0; y < map.Length; y++)
            {
                for (var x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] is Direction.Step)
                    {
                        count++;
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
            // solve puzzle
        }
        else
        {
            _logger.LogError("File not found");
        }
    }
}
