using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class NotEmptyAttribute : ValidationAttribute
{
    internal const string ErrorMessageFormat = "The field {0} must be an enumerable that contains at least one element";

    public NotEmptyAttribute()
        : base(ErrorMessageFormat)
    {
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        ICollection<object?> collection => collection.Count > 0,
        ICollection collection => collection.Count > 0,
        IEnumerable<object?> enumerable => enumerable.Any(),
        IEnumerable enumerable => enumerable.Cast<object?>().Any(),
        _ => false,
    };
}