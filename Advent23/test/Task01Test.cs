using FluentAssertions;
using Xunit;

namespace Advent23;

public class Task01Test
{
    [Fact]
    public void Test_empty_string()
    {
        const string input = "";
        var output = Task01.ParseLine(input);
        output.Should().BeNegative();
    }

    [Fact]
    public void Test_single_number()
    {
        const string input = "1";
        var output = Task01.ParseLine(input);
        output.Should().Be(11);
    }

    [Fact]
    public void Test_two_numbers()
    {
        const string input = "12";
        var output = Task01.ParseLine(input);
        output.Should().Be(12);
    }

    [Fact]
    public void Test_more_than_two_numbers()
    {
        const string input = "123";
        var output = Task01.ParseLine(input);
        output.Should().Be(13);
    }

    [Fact]
    public void Test_more_than_two_numbers_with_repeating_digits()
    {
        const string input = "112233";
        var output = Task01.ParseLine(input);
        output.Should().Be(13);
    }

    [Fact]
    public void Test_more_than_two_numbers_with_other_characters()
    {
        const string input = "1a2b3c";
        var output = Task01.ParseLine(input);
        output.Should().Be(13);
    }

    [Fact]
    public void Test_multiple_lines()
    {
        const string input = "1\n2\n3";
        var output = Task01.ParseDocument(input);
        output.Should().BeEquivalentTo(new List<int> { 11, 22, 33 });
    }

    [Fact]
    public void Test_multiple_lines_with_empty_lines()
    {
        const string input = "1\n\n2\n3";
        var output = Task01.ParseDocument(input);
        output.Should().BeEquivalentTo(new List<int> { 11, 22, 33 });
    }

    [Fact]
    public void Test_multiple_lines_with_empty_lines_and_other_characters()
    {
        const string input = "1\n\n2\n3\na\nb\nc";
        var output = Task01.ParseDocument(input);
        output.Should().BeEquivalentTo(new List<int> { 11, 22, 33 });
    }

    [Fact]
    public void Test_multiple_lines_with_various_characters_in_each_line()
    {
        const string input = "1a2b3c\n4d5e6f\n7g8h9i";
        var output = Task01.ParseDocument(input);
        output.Should().BeEquivalentTo(new List<int> { 13, 46, 79 });
    }

    [Fact]
    public void Test_parsing_a_small_file()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\res\\task01_test-small.txt";
        var output = Task01.ParseFile(filePath);
        output.Should().BeEquivalentTo(new List<int> { 12, 38, 15, 77 });
    }

    [Fact]
    public void Test_parsing_a_large_file()
    {
        var filePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\res\\task01_input.txt";
        var output = Task01.ParseFile(filePath);
        output.Sum().Should().Be(54877);
    }
}