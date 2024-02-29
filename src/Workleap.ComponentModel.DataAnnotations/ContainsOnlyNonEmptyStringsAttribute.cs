using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ContainsOnlyNonEmptyStringsAttribute : ValidationAttribute
{
    internal const string ErrorMessageFormat = "The field {0} must be a collection that contains only non-empty Strings";
    
    public ContainsOnlyNonEmptyStringsAttribute() : base(ErrorMessageFormat)
    {
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        IEnumerable<string> enumerable => enumerable.All(x => !string.IsNullOrWhiteSpace(x)),
        _ => false,
    };
}