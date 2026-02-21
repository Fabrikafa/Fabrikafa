using System.Globalization;

namespace FabrikafaTests;

internal static class Helper
{
    // Helper to run assertions under invariant culture so numeric ToString() is predictable
    internal static T RunInvariantCulture<T>(Func<T> action)
    {
        var original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            return action();
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }
}
