using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class GuidAttribute : ValidationAttribute
{
    internal const string ErrorMessageNonEmptyPart = " non-empty";
    internal const string ErrorMessageWithoutFormatFormat = "The {0} field must be a well-formatted{1} GUID";
    internal const string ErrorMessageWithFormatFormat = "The {0} field must be a well-formatted{1} GUID of format '{2}'";

    public GuidAttribute()
    {
        this.Format = null;
        this.AllowEmpty = true;
    }

    public GuidAttribute(string format)
    {
        this.Format = format;
        this.AllowEmpty = true;
    }

    public string? Format { get; }

    public bool AllowEmpty { get; set; }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        Guid valueAsGuid => this.IsValidGuid(valueAsGuid),
        string valueAsString => this.IsValidGuid(valueAsString),
        _ => false,
    };

    private bool IsValidGuid(string valueAsString)
    {
        return this.Format is null
            ? Guid.TryParse(valueAsString, out Guid guid) && this.IsValidGuid(guid)
            : Guid.TryParseExact(valueAsString, this.Format, out guid) && this.IsValidGuid(guid);
    }

    private bool IsValidGuid(Guid guid)
    {
        return this.AllowEmpty || guid != Guid.Empty;
    }

    public override string FormatErrorMessage(string name)
    {
        if (this.ErrorMessage != null)
        {
            return this.ErrorMessage;
        }

        var emptiness = this.AllowEmpty ? string.Empty : ErrorMessageNonEmptyPart;
        return this.Format == null
            ? string.Format(CultureInfo.InvariantCulture, ErrorMessageWithoutFormatFormat, name, emptiness)
            : string.Format(CultureInfo.InvariantCulture, ErrorMessageWithFormatFormat, name, emptiness, this.Format);
    }
}