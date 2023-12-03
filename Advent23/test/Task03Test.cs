using FluentAssertions;
using Xunit;

namespace Advent23;

public class Task03Test
{
    [Fact]
    public void Test_parsing_empty_field()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_empty.txt");

        partNumbers.Count.Should().Be(0);
    }

    [Fact]
    public void Test_parsing_a_file_with_a_single_one_digit_number()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_single-one-digit.txt");

        partNumbers.Count.Should().Be(1);
        partNumbers[0].Number.Should().Be(1);
        partNumbers[0].AdjacentChars.Count.Should().Be(0);
    }

    [Fact]
    public void Test_parsing_a_file_with_a_single_multi_digit_number()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_single-multi-digit.txt");

        partNumbers.Count.Should().Be(1);
        partNumbers[0].Number.Should().Be(123);
        partNumbers[0].AdjacentChars.Count.Should().Be(0);
    }

    [Fact]
    public void Test_parsing_a_file_with_surrounding_chars()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_surrounding-chars.txt");

        partNumbers.Count.Should().Be(1);
        partNumbers[0].Number.Should().Be(123);
        partNumbers[0].AdjacentChars.Count.Should().Be(2);
    }

    [Theory]
    [InlineData("task03_valid-char-to-the-left.txt")]
    [InlineData("task03_valid-char-to-the-right.txt")]
    [InlineData("task03_valid-char-above.txt")]
    [InlineData("task03_valid-char-below.txt")]
    [InlineData("task03_valid-char-ul.txt")]
    [InlineData("task03_valid-char-ur.txt")]
    [InlineData("task03_valid-char-dl.txt")]
    [InlineData("task03_valid-char-dr.txt")]
    public void Test_parsing_a_file_with_valid_char_to_the_left_of_number(string fileName)
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/" + fileName);

        partNumbers.Count.Should().Be(1);
        partNumbers[0].Number.Should().Be(123);
        partNumbers[0].AdjacentChars.Count.Should().Be(12);
        partNumbers[0].ContainsNonDotCharacter().Should().BeTrue();
    }

    [Fact]
    public void Test_parsing_a_file_with_multiple_numbers()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_multiple-numbers.txt");

        partNumbers.Count.Should().Be(2);
        partNumbers[0].Number.Should().Be(123);
        partNumbers[0].AdjacentChars.Count.Should().Be(12);
        partNumbers[0].ContainsNonDotCharacter().Should().BeTrue();
        partNumbers[1].Number.Should().Be(456);
        partNumbers[1].AdjacentChars.Count.Should().Be(5);
        partNumbers[1].ContainsNonDotCharacter().Should().BeTrue();
    }

    [Fact]
    public void Test_parsing_a_file_with_multiple_numbers_and_surrounding_chars()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_multiple-numbers-surrounding-chars.txt");

        partNumbers.Count.Should().Be(2);
        partNumbers[0].Number.Should().Be(123);
        partNumbers[0].AdjacentChars.Count.Should().Be(12);
        partNumbers[0].ContainsNonDotCharacter().Should().BeTrue();
        partNumbers[1].Number.Should().Be(456);
        partNumbers[1].AdjacentChars.Count.Should().Be(5);
        partNumbers[1].ContainsNonDotCharacter().Should().BeTrue();
    }

    [Fact]
    public void Test_demo_input()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_demo.txt");

        partNumbers.Where(p => p.ContainsNonDotCharacter()).Sum(p => p.Number).Should().Be(4361);
    }

    [Fact]
    public void Test_puzzle_input()
    {
        var fileParser = new FileParser();
        var partNumbers = fileParser.ParseFile(AppDomain.CurrentDomain.BaseDirectory + "../../../res/task03/task03_input.txt");

        partNumbers.Where(p => p.ContainsNonDotCharacter()).Sum(p => p.Number).Should().Be(531561);
    }
}
