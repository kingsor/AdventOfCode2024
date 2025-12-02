using ConsoleMainApp.Helpers;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace ConsoleMainApp.TaskRunners;

internal class PuzzleSolverDay08 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay08> _logger;

    public PuzzleSolverDay08(ILogger<PuzzleSolverDay08> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        SolvePuzzle("./data/day08/test1.txt");

        SolvePuzzle("./data/day08/input1.txt");
    }

    private void SolvePuzzle(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var map = FileHelper.GetBytesMap(inputFile);

            var antennas = new Dictionary<char, List<(int x, int y)>>();

            var result1 = 0L;
            var result2 = 0L;

            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[row].Length; col++)
                {
                    var content = (char)map[row][col];
                    var position = (col, row);
                    
                    if (content != '.')
                    {
                        if (!antennas.ContainsKey(content))
                        {
                            antennas.Add(content, new List<(int, int)>());
                        }

                        antennas[content].Add(position);
                    }
                }
            }

            HashSet<(int x, int y)> antinodes = new();

            foreach (var antenna in antennas)
            {
                var points = antenna.Value;
                for (var idx1 = 0; idx1 < points.Count; idx1++)
                {
                    for (var idx2 = idx1 + 1; idx2 < points.Count; idx2++)
                    {
                        (int x,int y) diff = (points[idx2].x - points[idx1].x, points[idx2].y - points[idx1].y);
                        
                        antinodes.Add((points[idx1].x - diff.x, points[idx1].y - diff.y));
                        antinodes.Add((points[idx2].x + diff.x, points[idx2].y + diff.y));
                    }
                }
            }

            result1 = antinodes.Where(a => IsInMap(a, map)).Count();

            _logger.LogInformation($"Result1: {result1}");

            antinodes.Clear();

            foreach (var antenna in antennas)
            {
                var points = antenna.Value;
                for (var idx1 = 0; idx1 < points.Count; idx1++)
                {
                    for (var idx2 = idx1 + 1; idx2 < points.Count; idx2++)
                    {
                        (int x, int y) diff = (points[idx2].x - points[idx1].x, points[idx2].y - points[idx1].y);

                        var curPos = points[idx1];
                        do
                        {
                            antinodes.Add(curPos);
                            curPos = (curPos.x - diff.x, curPos.y - diff.y);

                        } while (IsInMap(curPos, map));

                        curPos = points[idx2];
                        do
                        {
                            antinodes.Add(curPos);
                            curPos = (curPos.x + diff.x, curPos.y + diff.y);
                        } while (IsInMap(curPos, map));
                    }
                }
            }

            result2 = antinodes.Count();

            _logger.LogInformation($"Result2: {result2}");

        }
        else
        {
            _logger.LogError("File not found");
        }
    }

    private bool IsInMap((int x, int y) point, byte[][] map)
    {
        return point.x >= 0 && point.x < map[0].Length && point.y >= 0 && point.y < map.Length;
    }
    
}
