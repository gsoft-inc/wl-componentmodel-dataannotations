using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workleap.ComponentModel.DataAnnotations;

// N'importe quoi
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ContainsNonEmptyStringAttribute : ValidationAttribute
{
    internal const string ErrorMessageFormat = "The field {0} must be a collection that contains at least one non-empty String";
    
    public ContainsNonEmptyStringAttribute() : base(ErrorMessageFormat)
    {
    }

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        IEnumerable<string> enumerable => enumerable.Any(x => !string.IsNullOrWhiteSpace(x)),
        _ => false,
    };
}