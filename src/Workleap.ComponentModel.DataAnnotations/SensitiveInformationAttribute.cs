using System;

namespace Workleap.ComponentModel.DataAnnotations;

/// <summary>
/// Indicates that a property contains sensitive information, such as personally identifiable information (PII),
/// or any other information that might result in loss of an advantage or level of security if disclosed to others.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SensitiveInformationAttribute : Attribute
{
    public SensitiveInformationAttribute(SensitivityScope scope)
    {
        this.Scope = scope;
    }

    public SensitivityScope Scope { get; }
}