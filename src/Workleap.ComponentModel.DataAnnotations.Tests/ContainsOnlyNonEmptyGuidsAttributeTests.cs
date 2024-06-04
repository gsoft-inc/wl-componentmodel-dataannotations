using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public sealed class ContainsOnlyNonEmptyGuidsAttributeTests
{
    [Theory]
    [MemberData(nameof(ValidData))]
    public void Given_ValidGuids_When_Validate_Then_Valid(object? inputs)
    {
        var attr = new ContainsOnlyNonEmptyGuidsAttribute();
        Assert.True(attr.IsValid(inputs));
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void Given_InvalidGuids_When_Validate_Then_Invalid(object? inputs)
    {
        var attr = new ContainsOnlyNonEmptyGuidsAttribute();
        Assert.False(attr.IsValid(inputs));
    }

    [Fact]
    public void Validator_TryValidateObject_Returns_The_Expected_Error_Message_When_Validation_Fails()
    {
        var something = new SomeClass();
        var expectedErrorMessage = string.Format(CultureInfo.InvariantCulture, ContainsOnlyNonEmptyGuidsAttribute.ErrorMessageFormat, nameof(SomeClass.Values));

        var results = new List<ValidationResult>();
        var context = new ValidationContext(something, serviceProvider: null, items: null);
        var isValid = Validator.TryValidateObject(something, context, results, validateAllProperties: true);

        Assert.False(isValid);
        var result = Assert.Single(results);
        Assert.NotNull(result.ErrorMessage);
        Assert.Equal(expectedErrorMessage, result.ErrorMessage);
    }

    public static TheoryData<object?> ValidData()
    {
        return new TheoryData<object?>
        {
            null,
            Array.Empty<Guid>(),
            Array.Empty<Guid?>(),
            Array.Empty<string>(),
            Array.Empty<string?>(),
            new Guid[] { new("f8daff85-4393-42ae-9ab5-8620ab20c8da") },
            new string[] { "d78b48f9-37b8-47dd-8e47-0325dd3e7899" }
        };
    }

    public static TheoryData<object?> InvalidData()
    {
        return new TheoryData<object?>
        {
            new Guid[] { default },
            new Guid?[] { default },
            new string[] { string.Empty },
            new string?[] { default },
            new Guid[] { Guid.Empty },
            new string[] { "00000000-0000-0000-0000-000000000000" },
            new int[] { 0 },
            new Guid[] { default, new("846398d2-416d-4f05-86b0-431e11117abc") },
            new Guid[] { Guid.Empty, new("08bfec4a-da43-40de-a15a-0d10f227e6b7") },
            new Guid?[] { default, new Guid("2dd81281-7498-411c-aea1-5cec96f2dd8d") },
            new string[] { "00000000-0000-0000-0000-000000000000", "7d8c61f7-a334-4599-b801-45f0934099a4" },
            new string[] { string.Empty, "7c3cdfba-4466-44ab-bb95-da02eac67380" },
            new string?[] { default, "f8daff85-4393-42ae-9ab5-8620ab20c8da" }
        };
    }

    private sealed class SomeClass
    {
        [ContainsOnlyNonEmptyGuids]
        public Guid[] Values { get; set; } = new[] { Guid.Empty };
    }
}