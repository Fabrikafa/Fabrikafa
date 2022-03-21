using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrikafa;

public static class Extensions
{

    #region ToKiloFormat

    public static string ToKiloFormat(this int Number)
    {
        return ToKiloFormat(Number.ToString());
    }

    public static string ToKiloFormat(this double Number)
    {
        return ToKiloFormat(Number.ToString());
    }

    public static string ToKiloFormat(this string Number)
    {
        double number = Convert.ToDouble(Number);
        double resultNumber = number;
        string resultSymbol = string.Empty;

        if (number >= 10000)
        {
            resultSymbol = "k";
            resultNumber = Math.Round(number / 1000, 2);
        }

        if (number >= 1000000)
        {
            resultSymbol = "m";
            resultNumber = Math.Round(number / 1000000, 2);
        }

        if (number >= 1000000000)
        {
            resultSymbol = "b";
            resultNumber = Math.Round(number / 1000000000, 2);
        }

        if (number >= 1000000000000)
        {
            resultSymbol = "t";
            resultNumber = Math.Round(number / 1000000000000, 2);
        }

        string retVal = $"{resultNumber}{resultSymbol}";
        return retVal;
    }

    #endregion

    public static string RemoveAllWhitespaces(this string str)
    {
        return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
    }
}
