using System.Text.RegularExpressions;

namespace Advent23;

public class Roll
{
    public Roll(string rollData)
    {
        var matches = Regex.Matches(rollData, @"(-?\d+) (\w+)");

        foreach (Match match in matches)
        {
            int count = int.Parse(match.Groups[1].Value);

            if (count < 0)
                throw new ArgumentException("Count cannot be negative");

            switch (match.Groups[2].Value.ToLower())
            {
                case "blue":
                    if (Blue != 0)
                        throw new ArgumentException("Cannot have two blue values in one roll");
                    Blue += count;
                    break;
                case "red":
                    if (Red != 0)
                        throw new ArgumentException("Cannot have two red values in one roll");
                    Red += count;
                    break;
                case "green":
                    if (Green != 0)
                        throw new ArgumentException("Cannot have two green values in one roll");
                    Green += count;
                    break;
            }
        }

        if (Blue == 0 && Red == 0 && Green == 0)
            throw new ArgumentException("Roll cannot be empty");
    }

    public Roll(int blue, int red, int green)
    {
        Blue = blue;
        Red = red;
        Green = green;
    }

    public int Blue { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
}

public class SingleGame
{
    public int GameIndex { get; set; }
    public List<Roll> Rolls { get; set; } = new List<Roll>();

    public Roll GetMaxValues()
    {
        return new Roll(
            Rolls.Max(r => r.Blue),
            Rolls.Max(r => r.Red),
            Rolls.Max(r => r.Green)
        );
    }

    public bool IsValid(Roll maxValues)
    {
        return GetMaxValues().Blue <= maxValues.Blue
               && GetMaxValues().Red <= maxValues.Red
               && GetMaxValues().Green <= maxValues.Green;
    }
}

public class Games
{
    private Games(List<SingleGame> singleGames, Roll maxValues)
    {
        SingleGames = singleGames;
        MaxValues = maxValues;
    }

    public List<SingleGame>? SingleGames { get; private set; }
    public Roll MaxValues { get; private set; }

    public static Games ParseFromFile(string filePath, Roll maxValues)
    {
        var singleGames = new List<SingleGame>();

        string[] lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var singleGame = new SingleGame();
            var gameData = line.Split(':');
            singleGame.GameIndex = int.Parse(Regex.Match(gameData[0], @"\d+").Value);

            var rollsData = gameData[1].Split(';');

            foreach (var rollData in rollsData)
                singleGame.Rolls.Add(new Roll(rollData));

            singleGames.Add(singleGame);
        }

        return new Games(singleGames, maxValues);
    }

    public static Games ParseFromString(string gamesData, Roll maxValues)
    {
        var singleGames = new List<SingleGame>();

        var games = gamesData.Split("\r\n");

        foreach (var game in games)
        {
            var singleGame = new SingleGame();
            var gameData = game.Split(':');
            singleGame.GameIndex = int.Parse(Regex.Match(gameData[0], @"\d+").Value);

            var rollsData = gameData[1].Split(';');

            foreach (var rollData in rollsData)
                singleGame.Rolls.Add(new Roll(rollData));

            singleGames.Add(singleGame);
        }

        return new Games(singleGames, maxValues);
    }

    public static Games Create(List<SingleGame> singleGames, Roll maxValues)
    {
        return new Games(singleGames, maxValues);
    }

    public int GetIdxSumOfValidGames()
    {
        return SingleGames.Where(g => g.IsValid(MaxValues)).Sum(g => g.GameIndex);
    }
}
