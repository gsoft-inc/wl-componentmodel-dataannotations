using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class GuidAttribute : ValidationAttribute
{
    internal const string ErrorMessageNonEmptyPart = " non-empty";
    internal const string ErrorMessageWithoutFormatFormat = "The {{0}} field must be a well-formatted{0} GUID";
    internal const string ErrorMessageWithFormatFormat = "The {{0}} field must be a well-formatted{0} GUID of format '{1}'";

    public GuidAttribute(string? format = null, bool allowEmpty = true)
        : base(() => ErrorMessageFormatAccessor(format, allowEmpty))
    {
        this.Format = format;
        this.AllowEmpty = allowEmpty;
    }

    public string? Format { get; }

    public bool AllowEmpty { get; }

    private static string ErrorMessageFormatAccessor(string? format, bool allowEmpty)
    {
        var emptiness = allowEmpty ? string.Empty : ErrorMessageNonEmptyPart;
        return format == null
            ? string.Format(CultureInfo.InvariantCulture, ErrorMessageWithoutFormatFormat, emptiness)
            : string.Format(CultureInfo.InvariantCulture, ErrorMessageWithFormatFormat, emptiness, format);
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        Guid valueAsGuid => this.IsValidGuid(valueAsGuid),
        string valueAsString => this.IsValidGuid(valueAsString),
        _ => false,
    };

    private bool IsValidGuid(string valueAsString)
    {
        Guid guid;
        return this.Format is null
            ? Guid.TryParse(valueAsString, out guid) && this.IsValidGuid(guid)
            : Guid.TryParseExact(valueAsString, this.Format, out guid) && this.IsValidGuid(guid);
    }

    private bool IsValidGuid(Guid guid)
    {
        return this.AllowEmpty || guid != Guid.Empty;
    }
}