using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class TimeSpanAttribute : ValidationAttribute
{
    internal const string ErrorMessageWithoutFormatFormat = "The {0} field must be a well-formatted TimeSpan";
    internal const string ErrorMessageWithFormatFormat = "The {{0}} field must be a well-formatted TimeSpan of format '{0}'";

    public TimeSpanAttribute(string? format = null)
        : base(() => ErrorMessageFormatAccessor(format))
    {
        this.Format = format;
        this.UseInvariantCulture = false;
    }

    public string? Format { get; }

    public bool UseInvariantCulture { get; set; }

    private static string ErrorMessageFormatAccessor(string? format)
    {
        return format == null ? ErrorMessageWithoutFormatFormat : string.Format(CultureInfo.InvariantCulture, ErrorMessageWithFormatFormat, format);
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        string valueAsString => this.IsValidTimeSpan(valueAsString),
        _ => false,
    };

    private bool IsValidTimeSpan(string valueAsString)
    {
        var culture = this.UseInvariantCulture ? CultureInfo.InvariantCulture : null;
        return this.Format is null ? TimeSpan.TryParse(valueAsString, culture, out _) : TimeSpan.TryParseExact(valueAsString, this.Format, culture, out _);
    }
}