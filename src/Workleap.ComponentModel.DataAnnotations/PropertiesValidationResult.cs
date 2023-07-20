using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workleap.ComponentModel.DataAnnotations;

internal sealed class PropertiesValidationResult : ValidationResult
{
    public PropertiesValidationResult(string errorMessage, string memberName, IReadOnlyList<ValidationResult> innerResults)
        : base(errorMessage, new[] { memberName })
    {
        this.InnerResults = innerResults;
    }

    public IReadOnlyList<ValidationResult> InnerResults { get; }
}