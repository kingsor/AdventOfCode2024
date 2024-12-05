using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay5 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay5> _logger;

    public PuzzleSolverDay5(ILogger<PuzzleSolverDay5> logger)
    {
        _logger = logger;
    }

    public void Run(bool solveFirst = true)
    {
        if (solveFirst)
        {
            SolvePuzzleOne("./data/day5/test1.txt");

            SolvePuzzleOne("./data/day5/input1.txt");
        }

        SolvePuzzleTwo("./data/day5/test2.txt");

        SolvePuzzleTwo("./data/day5/input2.txt");
    }

    private void SolvePuzzleOne(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var lines = File.ReadAllLines(inputFile);

            List<(int, int)> orderRules = new List<(int, int)>();
            List<int[]> updates = new List<int[]>();
            List<int[]> notCorrectUpdates = new List<int[]>();

            var rules = true;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    rules = false;
                    continue;
                }

                if (rules)
                {
                    var values = line.Split('|');
                    orderRules.Add((Convert.ToInt32(values[0]), Convert.ToInt32(values[1])));
                }
                else
                {
                    var update = line.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
                    updates.Add(update);
                }
            }

            int result = 0;

            foreach (var pageUpdate in updates)
            {
                if(CheckPageUpdate(pageUpdate, orderRules, out int middlePage))
                {
                    result += middlePage;
                }
                else
                {
                    notCorrectUpdates.Add(pageUpdate);
                }
            }

            _logger.LogInformation($"Result One: {result}");

            result = 0;

            foreach (var pageUpdate in notCorrectUpdates)
            {
                if (FixAndCheckPageUpdate(pageUpdate, orderRules, out int middlePage))
                {
                    result += middlePage;
                    _logger.LogInformation($"Fixed update: {string.Join(",", pageUpdate)}");
                }
            }

            _logger.LogInformation($"Result Two: {result}");

        }
        else
        {
            _logger.LogError("File not found");
        }
    }

    private bool FixAndCheckPageUpdate(int[] pageUpdate, List<(int, int)> orderRules, out int middlePage)
    {
        middlePage = 0;

        bool isRightOrder = true;

        _logger.LogInformation($"Not correct update: {string.Join(",", pageUpdate)}");

        while(!CheckPageUpdate(pageUpdate, orderRules, out middlePage))
        {
            for (int idx = 0; idx < pageUpdate.Length - 1; idx++)
            {
                if (orderRules.Contains((pageUpdate[idx + 1], pageUpdate[idx])))
                {
                    (pageUpdate[idx], pageUpdate[idx + 1]) = (pageUpdate[idx + 1], pageUpdate[idx]);
                    _logger.LogInformation($"Fixing update: {string.Join(",", pageUpdate)}");
                }
            }
        }

        var middleIdx = pageUpdate.Length / 2;
        middlePage = pageUpdate[middleIdx];

        return isRightOrder;
    }

    private bool CheckPageUpdate(int[] pageUpdate, List<(int, int)> orderRules, out int middlePage)
    {
        middlePage = 0;

        bool isRightOrder = true;

        for(int idx = 0; idx < pageUpdate.Length - 1; idx++)
        {
            isRightOrder &= orderRules.Contains((pageUpdate[idx], pageUpdate[idx+1]));
        }

        if(isRightOrder)
        {
            var middleIdx = pageUpdate.Length / 2;
            middlePage = pageUpdate[middleIdx];
        }

        return isRightOrder;
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
