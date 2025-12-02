using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners;

public class TaskRunner
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TaskRunner> _logger;

    public TaskRunner(IServiceProvider serviceProvider, ILogger<TaskRunner> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void RunSolver(int dayNumber)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(dayNumber, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(dayNumber, 24);

        _logger.LogInformation($"Finding a puzzle solver for day {dayNumber:00}");

        var solver = GetPuzzleSolver(dayNumber);

        if (solver != null)
        {
            _logger.LogInformation($"Running the puzzle solver for day {dayNumber:00}");

            solver.Run();
        }
    }

    private IPuzzleSolver? GetPuzzleSolver(int dayNumber)
    {
        var typeName = $"ConsoleMainApp.TaskRunners.PuzzleSolverDay{dayNumber:00}";

        var solverType = Type.GetType(typeName);

        var solverInstance = _serviceProvider.GetService(solverType);

        return solverInstance as IPuzzleSolver;
    }
}
