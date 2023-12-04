using FluentAssertions;
using Xunit;

namespace Advent23;

public class Task04Test
{
    // part 1

    [Fact]
    public void Puzzle_one_test_parsing_a_line()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_parsing-line.txt";
        var output = Task04.ProcessFile(filePath, 1);

        output.Count.Should().Be(1);

        output[0].WinningNumbers.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        output[0].MyNumbers.Should().BeEquivalentTo(new[] { 4, 5, 6 });
        output[0].MatchCount.Should().Be(0);
        output[0].Result.Should().Be(0);
    }

    [Fact]
    public void Puzzle_one_test_parsing_multiple_lines()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_parsing-multiple-lines.txt";
        var output = Task04.ProcessFile(filePath, 1);

        output.Count.Should().Be(2);

        output[0].WinningNumbers.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        output[0].MyNumbers.Should().BeEquivalentTo(new[] { 4, 5, 6 });
        output[0].MatchCount.Should().Be(0);
        output[0].Result.Should().Be(0);

        output[1].WinningNumbers.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        output[1].MyNumbers.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        output[1].MatchCount.Should().Be(3);
        output[1].Result.Should().Be(4);
    }

    [Fact]
    public void Puzzle_one_test_demo_input()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_demo.txt";
        var output = Task04.ProcessFile(filePath, 1);
        output.Sum(result => result.Result).Should().Be(13);
    }

    [Fact]
    public void Puzzle_one_test_puzzle_input()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_input.txt";
        var output = Task04.ProcessFile(filePath, 1);
        output.Sum(result => result.Result).Should().Be(22674);
    }

    // part 2

    [Fact]
    public void Puzzle_two_test_parsing_a_line()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_parsing-line.txt";
        var output = Task04.ProcessFile(filePath, 2);

        output.Count.Should().Be(1);
    }

    [Fact]
    public void Puzzle_two_test_parsing_multiple_lines()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_parsing-multiple-lines.txt";
        var output = Task04.ProcessFile(filePath, 2);

        output.Count.Should().Be(2);
    }

    [Fact]
    public void Puzzle_two_test_parsing_multiple_lines_with_one_line_repeated()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_parsing-multiple-lines-with-repeating.txt";
        var output = Task04.ProcessFile(filePath, 2);

        output.Count.Should().Be(3);
    }

    [Fact]
    public void Puzzle_two_test_demo_input()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_demo.txt";
        var output = Task04.ProcessFile(filePath, 2);
        output.Count.Should().Be(30);
    }

    [Fact]
    public void Puzzle_two_test_puzzle_input()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "../../../res/task04/task04_input.txt";
        var output = Task04.ProcessFile(filePath, 2);
        output.Count.Should().Be(5747443);
    }
}