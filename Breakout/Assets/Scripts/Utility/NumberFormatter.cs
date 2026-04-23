using UnityEngine;
using System.Globalization;

public static class NumberFormatter
{
    private static readonly string[] suffixes =
    {
        "", "K", "M", "B", "T"
    };

    private static readonly CultureInfo culture = new CultureInfo("nl-NL");

    public static string FormatNumber(double value)
    {
        if (value < 1000)
            return value.ToString("0.##", culture);

        int suffixIndex = 0;

        while (value >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            value /= 1000;
            suffixIndex++;
        }

        return value.ToString("0.##", culture) + suffixes[suffixIndex];
    }
}