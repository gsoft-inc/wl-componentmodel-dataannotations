using System.Globalization;

namespace GSoft.ComponentModel.DataAnnotations.Tests;

internal static class StringExtensions
{
    public static string FormatInvariant(this string format, params object?[] args)
        => string.Format(CultureInfo.InvariantCulture, format, args);
}