using System;

namespace ShareGate.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class StartsWithAttribute : TextBasedValidationAttribute
{
    public StartsWithAttribute(string text)
        : base(text)
    {
    }

    protected override bool IsValid(string value)
    {
        return value.StartsWith(this.Text, this.StringComparison);
    }
}