using Fabrikafa;

namespace FabrikafaTests;

public class ExtensionsTests
{
    [Fact]
    public void ToKiloFormatTest()
    {
        var stringInput = "1";
        var result = stringInput.ToKiloFormat();
        Assert.Equal("1", result);

        stringInput = "10";
        result = stringInput.ToKiloFormat();
        Assert.Equal("10", result);

        stringInput = "100";
        result = stringInput.ToKiloFormat();
        Assert.Equal("100", result);

        stringInput = "1000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("1000", result);

        stringInput = "10000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("10k", result);

        stringInput = "100000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("100k", result);

        stringInput = "1000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("1m", result);

        stringInput = "10000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("10m", result);

        stringInput = "100000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("100m", result);

        stringInput = "1000000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("1b", result);

        stringInput = "10000000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("10b", result);

        stringInput = "100000000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("100b", result);

        stringInput = "1000000000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("1t", result);

        stringInput = "10000000000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("10t", result);

        stringInput = "100000000000000";
        result = stringInput.ToKiloFormat();
        Assert.Equal("100t", result);

        stringInput = "4124124";
        result = stringInput.ToKiloFormat();
        Assert.Equal("4.12m", result);
    }

    [Fact]
    public void RemoveAllWhitespacesTest()
    {
        var stringInput = "     Some Sample     String To Text     Extension Methods     ";
        var result = stringInput.RemoveAllWhitespaces();

        Assert.Equal("SomeSampleStringToTextExtensionMethods", result);
    }

    [Fact]
    public void GetInitialsTest()
    {
        string stringSingleInitial = "Ozan";
        string stringDoubleInitial = "Ozan Bayram";
        string stringTripleInitial = "Ozan Kutlu Bayram";
        string stringMoreThan3Initial = "Ozan Kutlu Bayram ve Mahdumlari";

        var resultSingleInitial = stringSingleInitial.GetInitials();
        var resultDoubleInitial = stringDoubleInitial.GetInitials();
        var resultTripleInitial = stringTripleInitial.GetInitials();
        var resultMoreThan3Initial = stringMoreThan3Initial.GetInitials();

        Assert.Equal("O", resultSingleInitial);
        Assert.Equal("OB", resultDoubleInitial);
        Assert.Equal("OK", resultTripleInitial);
        Assert.Equal("OK", resultMoreThan3Initial);
    }

    [Fact]
    public void FilterInputTest()
    {
        var stringInput = "<tag onclick=\"jsmethod();\">";
        var result = stringInput.FilterInput();
        Assert.Equal("tag onclick jsmethod", result);

        stringInput = "<tag onclick='jsmethod();'>";
        result = stringInput.FilterInput();
        Assert.Equal("tag onclick`jsmethod`", result);
    }  
}
