using FluentAssertions;
using Xunit;

namespace Advent23;

public class Task02Test
{
    [Fact]
    public void Test_parsing_a_roll()
    {
        var roll = new Roll("1 blue 2 red 3 green");

        roll.Blue.Should().Be(1);
        roll.Red.Should().Be(2);
        roll.Green.Should().Be(3);
    }

    [Fact]
    public void Test_parsing_a_roll_with_two_values_of_same_type()
    {
        Action act = () => new Roll("1 blue 2 blue 3 green");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_creating_an_empty_roll()
    {
        Action act = () => new Roll(0, 0, 0);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_parsing_a_roll_with_negative_values()
    {
        Action act = () => new Roll("1 blue -2 red 3 green");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_get_maximum_of_each_color_in_game()
    {
        var game = new SingleGame
        {
            GameIndex = 1,
            Rolls = new List<Roll>
            {
                new(1, 2, 3),
                new(4, 5, 6),
                new(7, 8, 9)
            }
        };

        var result = game.GetMaxValues();
        result.Should().BeEquivalentTo(new Roll(7, 8, 9));
    }

    [Fact]
    public void Test_parsing_games_from_string()
    {
        var input = "Game 1: 1 blue, 1 red\r\nGame 2: 1 blue, 1 green; 1 green, 1 blue, 1 red";

        var games = Games.ParseFromString(input, new Roll(1, 1, 1));

        games.Should().BeEquivalentTo(Games.Create(new List<SingleGame>
        {
            new()
            {
                GameIndex = 1,
                Rolls = new List<Roll>
                {
                    new(1, 1, 0)
                }
            },
            new()
            {
                GameIndex = 2,
                Rolls = new List<Roll>
                {
                    new(1, 0, 1),
                    new(1, 1, 1)
                }
            }
        }, new Roll(1, 1, 1)));
    }

    [Fact]
    public void Test_parsing_games_from_file()
    {
        var games = Games.ParseFromFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task02_test-small.txt", new Roll(1, 1, 1));

        games.Should().BeEquivalentTo(Games.Create(new List<SingleGame>
        {
            new()
            {
                GameIndex = 1,
                Rolls = new List<Roll>
                {
                    new(1, 1, 0)
                }
            },
            new()
            {
                GameIndex = 2,
                Rolls = new List<Roll>
                {
                    new(1, 0, 1)
                }
            }
        }, new Roll(1, 1, 1)));
    }

    [Fact]
    public void Test_if_single_games_are_identified_as_valid_and_invalid()
    {
        var games = Games.Create(new List<SingleGame>
        {
            new()
            {
                GameIndex = 1,
                Rolls = new List<Roll>
                {
                    new(1, 1, 0)
                }
            },
            new()
            {
                GameIndex = 2,
                Rolls = new List<Roll>
                {
                    new(1, 0, 1)
                }
            }
        }, new Roll(1, 1, 0));

        games.SingleGames.Should().HaveCount(2);
        games.SingleGames?[0].IsValid(games.MaxValues).Should().BeTrue();
        games.SingleGames?[1].IsValid(games.MaxValues).Should().BeFalse();
    }

    [Fact]
    public void Test_if_sum_of_indices_of_valid_games_is_calculated_correctly()
    {
        var games = Games.Create(new List<SingleGame>
        {
            new()
            {
                GameIndex = 1,
                Rolls = new List<Roll>
                {
                    new(1, 1, 0)
                }
            },
            new()
            {
                GameIndex = 2,
                Rolls = new List<Roll>
                {
                    new(1, 0, 1)
                }
            }
        }, new Roll(1, 1, 0));

        games.GetIdxSumOfValidGames().Should().Be(1);
    }

    [Fact]
    public void Test_if_sum_of_indices_of_valid_games_is_calculated_correctly_for_larger_input()
    {
        var games = Games.ParseFromFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task02.txt", new Roll(12, 13, 14));
        games.GetIdxSumOfValidGames().Should().Be(8);
    }

    [Fact]
    public void Test_puzzle_input()
    {
        var games = Games.ParseFromFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task02_input.txt", new Roll(14, 12, 13));
        games.GetIdxSumOfValidGames().Should().Be(2377);
    }
}
