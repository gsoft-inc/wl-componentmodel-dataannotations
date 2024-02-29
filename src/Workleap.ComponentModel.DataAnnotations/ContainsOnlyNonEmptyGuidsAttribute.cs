using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ContainsOnlyNonEmptyGuidsAttribute : ValidationAttribute
{
    internal const string ErrorMessageFormat = "The field {0} must be a collection that contains only non-empty GUIDs";

    public ContainsOnlyNonEmptyGuidsAttribute() : base(ErrorMessageFormat)
    {
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        IEnumerable<Guid> enumerable => enumerable.All(this.IsValidGuid),
        IEnumerable<Guid?> enumerable => enumerable.All(x => x != null && this.IsValidGuid(x.Value)),
        IEnumerable<string?> enumerable => enumerable.All(this.IsValidGuid),
        _ => false,
    };

    private bool IsValidGuid(string? valueAsString)
    {
        return Guid.TryParse(valueAsString, out var guid) && this.IsValidGuid(guid);
    }

    private bool IsValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}