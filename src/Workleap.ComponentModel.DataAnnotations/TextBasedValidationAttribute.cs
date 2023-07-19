using System;
using System.ComponentModel.DataAnnotations;

namespace Workleap.ComponentModel.DataAnnotations;

public abstract class TextBasedValidationAttribute : ValidationAttribute
{
    protected TextBasedValidationAttribute(string text)
    {
        this.Text = text ?? throw new ArgumentNullException(nameof(text));
        this.IgnoreCase = false;
    }

    public string Text { get; }

    public bool IgnoreCase { get; set; }

    protected StringComparison StringComparison => this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

    public override bool IsValid(object? value) => value switch
    {
        null => true,
        string valueAsString => this.IsValid(valueAsString),
        _ => false,
    };

    protected abstract bool IsValid(string value);
}