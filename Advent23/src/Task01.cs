namespace Advent23;

public class Task01
{
    public static int ParseLine(string input)
    {
        var allDigits = input.Where(char.IsDigit).Select(c => int.Parse(c.ToString())).ToList();

        return allDigits.Count == 0 ? -1 : allDigits[0] * 10 + allDigits[^1];
    }

    public static List<int> ParseDocument(string input)
    {
        return input.Split("\n").Select(ParseLine).Where(num => num != -1).ToList();
    }

    public static List<int> ParseFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        return ParseDocument(File.ReadAllText(filePath));
    }
}