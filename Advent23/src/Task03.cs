namespace Advent23;

public class PartNumber
{
    public int Number { get; set; }
    public List<char> AdjacentChars { get; set; } = new List<char>();

    public PartNumber(int number)
    {
        Number = number;
    }

    public bool ContainsNonDotCharacter()
    {
        return AdjacentChars.Exists(c => c != '.');
    }
}

public class FileParser
{
    public List<PartNumber> ParseFile(string filePath)
    {
        var partNumbers = new List<PartNumber>();
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (char.IsDigit(lines[i][j]))
                {
                    string numberStr = ExtractNumber(lines, i, j);
                    int number = int.Parse(numberStr);
                    var partNumber = new PartNumber(number);

                    partNumber.AdjacentChars = GetAdjacentChars(lines, i, j, numberStr.Length);

                    partNumbers.Add(partNumber);
                    j += numberStr.Length - 1; // Skip past the full number
                }
            }
        }

        return partNumbers;
    }

    private string ExtractNumber(string[] lines, int i, int j)
    {
        string numStr = "";
        while (j < lines[i].Length && char.IsDigit(lines[i][j]))
        {
            numStr += lines[i][j];
            j++;
        }
        return numStr;
    }

    private List<char> GetAdjacentChars(string[] lines, int row, int startCol, int length)
    {
        var adjacentChars = new List<char>();

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = startCol - 1; j <= startCol + length; j++)
            {
                // Skip the number itself
                if (i == row && (j >= startCol && j < startCol + length)) continue;

                if (i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length)
                {
                    adjacentChars.Add(lines[i][j]);
                }
            }
        }

        return adjacentChars;
    }
}
