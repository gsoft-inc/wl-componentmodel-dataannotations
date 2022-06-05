using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ValidatePropertiesAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return this.IsValid(value, new ValidationContext(value)) == ValidationResult.Success;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var validationResults = new List<ValidationResult>();

        if (Validator.TryValidateObject(value, new ValidationContext(value), validationResults, validateAllProperties: true))
        {
            return ValidationResult.Success;
        }

        var newValidationResults = new List<ValidationResult>(validationResults.Count);
        var parentMemberName = validationContext.MemberName ?? validationContext.DisplayName;
        var objectTypeName = validationContext.ObjectType.Name;

        foreach (var validationResult in UnwrapValidationResults(validationResults))
        {
            var prefixedMemberNames = validationResult.MemberNames.Select(x => parentMemberName + "." + x).ToArray();
            newValidationResults.Add(new ValidationResult(validationResult.ErrorMessage, prefixedMemberNames));
        }

        return new PropertiesValidationResult(FormatErrorMessage(objectTypeName, parentMemberName, newValidationResults), parentMemberName, newValidationResults);
    }

    private static IEnumerable<ValidationResult> UnwrapValidationResults(IEnumerable<ValidationResult> validationResults)
    {
        foreach (var validationResult in validationResults)
        {
            if (validationResult is PropertiesValidationResult aggregateValidationResult)
            {
                foreach (var unwrappedValidationResult in UnwrapValidationResults(aggregateValidationResult.InnerResults))
                {
                    yield return unwrappedValidationResult;
                }
            }
            else
            {
                yield return validationResult;
            }
        }
    }

    private static string FormatErrorMessage(string typeName, string fieldName, IReadOnlyCollection<ValidationResult> validationResults)
    {
        var sb = new StringBuilder();
        
        sb.Append("The ");
        sb.Append(fieldName);
        sb.Append(" field of type ");
        sb.Append(typeName);
        sb.Append(" is invalid:");
        
        if (validationResults.Count > 1)
        {
            sb.Append(Environment.NewLine);
        }
        else
        {
            sb.Append(' ');
        }

        var lineSeparator = string.Empty;
        foreach (var validationResult in validationResults)
        {
            sb.Append(lineSeparator);

            var memberSeparator = string.Empty;
            foreach (var memberName in validationResult.MemberNames)
            {
                if (memberSeparator.Length == 0)
                {
                    sb.Append(" - ");
                }

                sb.Append(memberSeparator);
                sb.Append(memberName);
                
                memberSeparator = ", ";
            }

            sb.Append(": ");
            sb.Append(validationResult.ErrorMessage);

            lineSeparator = Environment.NewLine;
        }

        return sb.ToString();
    }
}