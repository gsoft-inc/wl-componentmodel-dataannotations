using System.Globalization;

namespace ShareGate.ComponentModel.DataAnnotations.Tests;

internal static class StringExtensions
{
    public static string FormatInvariant(this string format, params object?[] args)
        => string.Format(CultureInfo.InvariantCulture, format, args);
}