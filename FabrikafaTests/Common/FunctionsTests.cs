using Fabrikafa.Common;

namespace FabrikafaTests.Common;

public class FunctionsTests
{
    [Fact]
    public void IsValidEmailTestWithValidEmail()
    {
        Assert.True(Functions.IsValidEmail("test@fabrikafa.com"), "test@fabrikafa.com is valid email.");
        Assert.True(Functions.IsValidEmail("test@fabrikafa.com.tr"), "test@fabrikafa.com.tr is valid email.");
        Assert.True(Functions.IsValidEmail("test@cut.lu"), "test@cut.lu is valid email.");
        Assert.False(Functions.IsValidEmail("testcut.lu"), "testcut.lu is valid email.");
    }
}
