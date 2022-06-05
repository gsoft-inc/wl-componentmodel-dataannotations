﻿using System.Globalization;

namespace GSoft.ComponentModel.DataAnnotations.Tests;

internal static class StringExtensions
{
    public static string FormatInvariant(this string format, object? arg)
        => string.Format(CultureInfo.InvariantCulture, format, arg);

    public static string FormatInvariant(this string format, object? arg0, object? arg1)
        => string.Format(CultureInfo.InvariantCulture, format, arg0, arg1);

    public static string FormatInvariant(this string format, object? arg0, object? arg1, object? arg2)
        => string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2);

    public static string FormatInvariant(this string format, params object?[] args)
        => string.Format(CultureInfo.InvariantCulture, format, args);
}