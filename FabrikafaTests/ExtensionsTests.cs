using System;
using System.Globalization;
using Fabrikafa;
using Xunit;

namespace FabrikafaTests;

public class ExtensionsTests
{


    [Theory]
    [InlineData("1", "1")]
    [InlineData("10", "10")]
    [InlineData("100", "100")]
    [InlineData("1000", "1000")]
    [InlineData("10000", "10k")]
    [InlineData("100000", "100k")]
    [InlineData("1000000", "1m")]
    [InlineData("10000000", "10m")]
    [InlineData("100000000", "100m")]
    [InlineData("1000000000", "1b")]
    [InlineData("10000000000", "10b")]
    [InlineData("100000000000", "100b")]
    [InlineData("1000000000000", "1t")]
    [InlineData("10000000000000", "10t")]
    [InlineData("100000000000000", "100t")]
    [InlineData("4124124", "4.12m")]
    public void ToKiloFormat_StringInputs_ReturnsExpected(string input, string expected)
    {
        var result = Helper.RunInvariantCulture(() => input.ToKiloFormat());
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(10000, "10k")]
    [InlineData(4124124, "4.12m")]
    public void ToKiloFormat_IntOverload_ReturnsExpected(int input, string expected)
    {
        var result = Helper.RunInvariantCulture(() => input.ToKiloFormat());
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1.0, "1")]
    [InlineData(10000.0, "10k")]
    [InlineData(4124124.0, "4.12m")]
    public void ToKiloFormat_DoubleOverload_ReturnsExpected(double input, string expected)
    {
        var result = Helper.RunInvariantCulture(() => input.ToKiloFormat());
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("     Some Sample     String To Text     Extension Methods     ", "SomeSampleStringToTextExtensionMethods")]
    [InlineData("\tLeading\tand\nNewLine  Spaces ", "LeadingandNewLineSpaces")]
    public void RemoveAllWhitespaces_RemovesAllKindsOfWhitespace(string input, string expected)
    {
        var result = input.RemoveAllWhitespaces();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("Ozan", "O")]
    [InlineData("Ozan Bayram", "OB")]
    [InlineData("Ozan Kutlu Bayram", "OK")]
    [InlineData("Ozan Kutlu Bayram ve Mahdumlari", "OK")]
    [InlineData("ozan kutlu", "OK")] // verifies casing normalization to upper invariant
    public void GetInitials_VariousNameFormats_ReturnsExpected(string input, string expected)
    {
        var result = input.GetInitials();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("<tag onclick=\"jsmethod();\">", "tag onclick jsmethod")]
    [InlineData("<tag onclick='jsmethod();'>", "tag onclick`jsmethod`")]
    [InlineData("a=b(c);d", "abcd")] // '=' '(' ')' and ';' removed; note result concatenation
    public void FilterInput_SanitizesStringsAsExpected(string input, string expected)
    {
        var result = input.FilterInput();
        Assert.Equal(expected, result);
    }
}
