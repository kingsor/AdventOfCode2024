using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace ConsoleMainApp.TaskRunners;

public class PuzzleSolverDay09 : IPuzzleSolver
{
    private readonly ILogger<PuzzleSolverDay09> _logger;

    public PuzzleSolverDay09(ILogger<PuzzleSolverDay09> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        SolvePuzzle("./data/day09/test1.txt");

        SolvePuzzle("./data/day09/input1.txt");
    }

    private void SolvePuzzle(string inputFile)
    {
        _logger.LogInformation("Solving puzzle one");

        _logger.LogInformation($"Input file: {inputFile}");

        if (File.Exists(inputFile))
        {
            var diskMap = File.ReadAllText(inputFile);

            _logger.LogInformation($"Disk Map: {diskMap}");

            List<int> diskBlocks = new List<int>();

            List<(int idx, int count, int content)> diskSectors = new();

            var idNumber = 0;
            var freeMarker = -1;

            for (int i = 0; i < diskMap.Length; i++)
            {
                var blockNum = int.Parse(diskMap[i].ToString());

                if (i % 2 == 0)
                {
                    // blockNum blocks file
                    var fileBlocks = Enumerable.Repeat(idNumber, blockNum);
                    diskBlocks.AddRange(fileBlocks);

                    var sectorIndex = diskBlocks.FindIndex(x => x == idNumber);

                    diskSectors.Add((idx: sectorIndex, count: blockNum, content: idNumber));

                    idNumber++;
                }
                else
                {
                    // free space
                    diskBlocks.AddRange(Enumerable.Repeat(freeMarker, blockNum));

                    if(blockNum > 0)
                    {
                        var sectorIndex = diskBlocks.FindIndex(x => x == freeMarker);

                        diskSectors.Add((idx: sectorIndex, count: blockNum, content: freeMarker));

                        freeMarker--;
                    }
                }
            }

            var strDiskBlocks = string.Join('|', diskBlocks.Select(x => x < 0 ? "." : x.ToString()));
            _logger.LogInformation($"{strDiskBlocks}");


            var result = SolvePartOne(diskBlocks.Select(x => x < 0 ? -1 : x).ToList());

            _logger.LogInformation($"Result 1: {result}");


            result = SolvePartTwo(diskBlocks.Select(x => x).ToList(), diskSectors);

            _logger.LogInformation($"Result 2: {result}");
        }
        else
        {
            _logger.LogError("File not found");
        }
    }

    private long SolvePartTwo(List<int> diskBlocks, List<(int idx, int count, int content)> diskSectors)
    {
        var result = 0L;

        var freeSectors = diskSectors.Where(s => s.content < 0).ToList();

        var fileSectors = diskSectors.Where(s => s.content >= 0).ToList();

        var movedFiles = new List<(int idx, int count, int content)>();

        var fileCount = fileSectors.Count;

        for (int fileIndex = fileCount - 1; fileIndex >= 0; fileIndex--)
        {
            var lastFile = fileSectors[fileIndex];

            for (int freeIndex = 0; freeIndex < freeSectors.Count; freeIndex++)
            {
                var freeSector = freeSectors[freeIndex];

                if (lastFile.count <= freeSector.count && lastFile.idx > freeSector.idx)
                {
                    movedFiles.Add((idx: freeSector.idx, count: lastFile.count, content: lastFile.content));
                    fileSectors.RemoveAt(fileIndex);
                    freeSectors[freeIndex] = (idx: freeSector.idx + lastFile.count, freeSector.count - lastFile.count, freeSector.content);
                    break;
                }
            }
        }

        var allFiles = fileSectors.Concat(movedFiles).OrderBy(f => f.idx).ToList();
        for (int i = 0; i < allFiles.Count; i++)
        {
            var file = allFiles[i];
            for (int blockId = 0; blockId < file.count; blockId++)
            {
                var position = file.idx + blockId;
                result += position * file.content;
            }
        }

        return result;
    }

    private long SolvePartOne(List<int> diskBlocks)
    {
        for (int j = 0; j < diskBlocks.Count; j++)
        {
            if (diskBlocks[j] == -2)
            {
                break;
            }

            if (diskBlocks[j] == -1)
            {
                var lastIdx = diskBlocks.FindLastIndex(x => x != -1 && x != -2);

                diskBlocks[j] = diskBlocks[lastIdx];
                diskBlocks[lastIdx] = -2;
            }
        }

        var strDiskBlocks = string.Join('|', diskBlocks.Select(x => x is -1 ? "." : x is -2 ? "*" : x.ToString()));
        _logger.LogInformation($"{strDiskBlocks}");

        diskBlocks = diskBlocks.Select(n => n == -2 ? -1 : n).ToList();

        strDiskBlocks = string.Join('|', diskBlocks.Select(x => x is -1 ? "." : x.ToString()));
        _logger.LogInformation($"{strDiskBlocks}");

        var result = 0L;

        for (int k = 0; k < diskBlocks.Count; k++)
        {
            if ((diskBlocks[k] != -1))
            {
                result += k * diskBlocks[k];
            }
            else
            {
                break;
            }
        }

        return result;
    }

}
