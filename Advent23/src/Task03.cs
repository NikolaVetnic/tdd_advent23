using Advent23;

namespace Advent23;

public class PartNumber
{
    public int Number { get; set; }
    public List<CharAndCoordinates> AdjacentChars { get; set; } = new List<CharAndCoordinates>();

    public PartNumber(int number)
    {
        Number = number;
    }

    public bool ContainsNonDotCharacter()
    {
        return AdjacentChars.Exists(c => c.Character != '.');
    }
}

public class CharAndCoordinates
{
    public char Character { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }

    public CharAndCoordinates(char character, int row, int col)
    {
        Character = character;
        Row = row;
        Col = col;
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

    private List<CharAndCoordinates> GetAdjacentChars(string[] lines, int row, int startCol, int length)
    {
        var adjacentChars = new List<CharAndCoordinates>();

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = startCol - 1; j <= startCol + length; j++)
            {
                // Skip the number itself
                if (i == row && (j >= startCol && j < startCol + length)) continue;

                if (i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length)
                {
                    adjacentChars.Add(new(lines[i][j], i, j));
                }
            }
        }

        return adjacentChars;
    }
}

public class GearFinder
{
    public static int FindGearsRatioSum(List<PartNumber> partNumbers)
    {
        var partNumberNextToAsterisk = partNumbers.Where(p => p.AdjacentChars.Exists(c => c.Character == '*')).ToList();

        var output = 0;

        for (int i = 0; i < partNumberNextToAsterisk.Count; i++)
        {
            var asteriskInFirstPart = partNumberNextToAsterisk[i].AdjacentChars.First(c => c.Character == '*');

            for (int j = i + 1; j < partNumberNextToAsterisk.Count; j++)
            {
                var asteriskInSecondPart = partNumberNextToAsterisk[j].AdjacentChars.First(c => c.Character == '*');

                var rowMatch = asteriskInFirstPart.Row == asteriskInSecondPart.Row;
                var colMatch = asteriskInFirstPart.Col == asteriskInSecondPart.Col;

                if (rowMatch && colMatch)
                {
                    output += partNumberNextToAsterisk[i].Number * partNumberNextToAsterisk[j].Number;
                    break;
                }
            }
        }

        return output;
    }
}