namespace Advent23;

public class Task04
{
    public static List<ScratchcardResult> ProcessFile(string filePath, int puzzlePart)
    {
        if (puzzlePart < 0 || 2 < puzzlePart)
            throw new ArgumentException("Puzzle part must be 1 or 2");

        string input = File.ReadAllText(filePath);

        return puzzlePart == 1 ? ProcessInput(input) : ProcessInputComplex(input);
    }

    private static List<ScratchcardResult> ProcessInput(string input)
    {
        var lines = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        var scratchcardResults = new List<ScratchcardResult>();

        foreach (var line in lines)
        {
            var parts = line.Substring(line.IndexOf(':') + 1).Trim().Split('|');
            var winningNumbers = parts[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var myNumbers = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var matchCount = myNumbers.Count(num => winningNumbers.Contains(num));
            var result = (int)Math.Pow(2, matchCount - 1);

            scratchcardResults.Add(new ScratchcardResult(winningNumbers, myNumbers, matchCount, result));
        }

        return scratchcardResults;
    }

    private static List<ScratchcardResult> ProcessInputComplex(string input)
    {
        var lines = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var linesAndCounts = lines.Select(line => new LineAndCount { Line = line, Count = 1 }).ToList();

        var scratchcardResults = new List<ScratchcardResult>();

        for (int i = 0; i < linesAndCounts.Count; i++)
        {
            for (int j = 0; j < linesAndCounts[i].Count; j++)
            {
                var dataPart = lines[i].Substring(lines[i].IndexOf(':') + 1).Trim();
                var parts = dataPart.Split('|');
                var winningNumbers = parts[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToList();
                var myNumbers = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToList();

                int matchCount = myNumbers.Count(num => winningNumbers.Contains(num));
                var result = (int)Math.Pow(2, matchCount - 1);

                for (int k = 0; k < matchCount; k++)
                {
                    if (i + k + 1 >= linesAndCounts.Count)
                        break;

                    linesAndCounts[i + k + 1].Count++;
                }

                scratchcardResults.Add(new ScratchcardResult(winningNumbers, myNumbers, matchCount, result));
            }
        }

        return scratchcardResults;
    }
}

public class ScratchcardResult
{
    public ScratchcardResult(List<int> winningNumbers, List<int> myNumbers, int matchCount, int result)
    {
        WinningNumbers = winningNumbers;
        MyNumbers = myNumbers;
        MatchCount = matchCount;
        Result = result;
    }

    public List<int> WinningNumbers { get; set; }
    public List<int> MyNumbers { get; set; }
    public int MatchCount { get; set; }
    public int Result { get; set; }
}

public class LineAndCount
{
    public string Line { get; set; }
    public int Count { get; set; }
}