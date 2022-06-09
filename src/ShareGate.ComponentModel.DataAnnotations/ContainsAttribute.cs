using System;
using System.Diagnostics.CodeAnalysis;

namespace ShareGate.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public sealed class ContainsAttribute : TextBasedValidationAttribute
{
    public ContainsAttribute(string text)
        : base(text)
    {
    }

    [SuppressMessage("Usage", "CA2249", Justification = "Older .NET framework does not support Contains with StringComparison")]
    protected override bool IsValid(string value)
    {
        return value.IndexOf(this.Text, this.StringComparison) >= 0;
    }
}