using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay10 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay10> _logger;

    public PuzzleSolverDay10(ILogger<PuzzleSolverDay10> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        SolvePuzzle("./data/day10/test1.txt");

        SolvePuzzle("./data/day10/input1.txt");
    }


    private void SolvePuzzle(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var lines = File.ReadAllLines(inputFile);

            List<MapPoint> zeroes = new List<MapPoint>();

            var map = new int[lines.Length][];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (x == 0)
                    {
                        map[y] = new int[lines[0].Length];
                    }

                    map[y][x] = lines[y][x] - '0';

                    if (map[y][x] == 0)
                    {
                        zeroes.Add(new MapPoint(y, x));
                    }
                }
            }

            var result = 0;
            foreach (var zero in zeroes)
            {
                result += GetScore(zero, map);
            }

            _logger.LogInformation($"Result: {result}");

            var result2 = 0;
            foreach (var zero in zeroes)
            {
                result2 += GetScorePart2(zero, map);
            }

            _logger.LogInformation($"Result2: {result2}");
        }
        else
        {
            _logger.LogError("File not found");
        }
    }

    private int GetScorePart2(MapPoint start, int[][] map)
    {
        var score = 0;
        Queue<MapPoint> queue = new Queue<MapPoint>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var value = map[current.X][current.Y];

            _logger.LogInformation($"current pos: ({current}) - value: {value}");

            if (value == 9)
            {
                _logger.LogInformation("New score");
                score++;
                continue;
            }

            foreach (var direction in Directions)
            {
                var next = current + direction;

                _logger.LogInformation($"next pos: ({next})");

                if (IsInMap(next, map) && map[next.X][next.Y] == (value + 1))
                {
                    queue.Enqueue(next);
                    _logger.LogInformation($">>> add to queue with value: {map[next.X][next.Y]}");
                }
            }
        }

        return score;
    }

    private int GetScore(MapPoint start, int[][] map)
    {
        var score = 0;
        HashSet<MapPoint> visited = new HashSet<MapPoint>();
        Queue<MapPoint> queue = new Queue<MapPoint>();
        visited.Add(start);
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var value = map[current.X][current.Y];

            _logger.LogInformation($"current pos: ({current}) - value: {value}");

            if (value == 9)
            {
                _logger.LogInformation("New score");
                score++;
                continue;
            }

            foreach (var direction in Directions)
            {
                var next = current + direction;

                _logger.LogInformation($"next pos: ({next})");

                if (IsInMap(next, map) && !visited.Contains(next) &&
                    map[next.X][next.Y] == (value + 1))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                    _logger.LogInformation($">>> add to queue with value: {map[next.X][next.Y]}");
                }
            }
        }

        return score;
    }

    private readonly MapPoint[] Directions =
        [
            new MapPoint(0, 1),  // ^
            new MapPoint(1, 0),  // >
            new MapPoint(0, -1), // v
            new MapPoint(-1, 0)  // <
        ];

    private bool IsInMap(MapPoint point, int[][] map)
    {
        return point.X >= 0 && point.X < map[0].Length && point.Y >= 0 && point.Y < map.Length;
    }

    public record struct MapPoint(int X, int Y)
    {
        public static MapPoint operator +(MapPoint a, MapPoint b) => new MapPoint(a.X + b.X, a.Y + b.Y);

        public static MapPoint operator -(MapPoint a, MapPoint b) => new MapPoint(a.X - b.X, a.Y - b.Y);

        public static MapPoint operator *(MapPoint point, int multiple) => new MapPoint(point.X * multiple, point.Y * multiple);

        public MapPoint Normalize() => new MapPoint(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

        public static implicit operator MapPoint((int X, int Y) tuple) => new MapPoint(tuple.X, tuple.Y);

        public int ManhattanDistance(MapPoint b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
    }
}
