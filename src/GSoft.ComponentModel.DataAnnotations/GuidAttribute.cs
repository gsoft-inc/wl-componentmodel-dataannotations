using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class GuidAttribute : ValidationAttribute
{
    internal const string ErrorMessageWithoutFormatFormat = "The {0} field must be a well-formatted GUID";
    internal const string ErrorMessageWithFormatFormat = "The {{0}} field must be a well-formatted GUID of format '{0}'";

    public GuidAttribute(string? format = null)
        : base(() => ErrorMessageFormatAccessor(format))
    {
        this.Format = format;
    }

    public string? Format { get; }

    private static string ErrorMessageFormatAccessor(string? format)
    {
        return format == null ? ErrorMessageWithoutFormatFormat : string.Format(CultureInfo.InvariantCulture, ErrorMessageWithFormatFormat, format);
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        string valueAsString => this.IsValidGuid(valueAsString),
        _ => false,
    };

    private bool IsValidGuid(string valueAsString)
    {
        return this.Format is null ? Guid.TryParse(valueAsString, out _) : Guid.TryParseExact(valueAsString, this.Format, out _);
    }
}