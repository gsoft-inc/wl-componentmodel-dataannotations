using System;

namespace ShareGate.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EndsWithAttribute : TextBasedValidationAttribute
{
    public EndsWithAttribute(string text)
        : base(text)
    {
    }

    protected override bool IsValid(string value)
    {
        return value.EndsWith(this.Text, this.StringComparison);
    }
}