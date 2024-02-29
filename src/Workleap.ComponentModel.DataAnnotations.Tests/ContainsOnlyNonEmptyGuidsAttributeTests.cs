using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public sealed class ContainsOnlyNonEmptyGuidsAttributeTests
{
    [Theory]
    [ClassData(typeof(ValidData))]
    public void Given_ValidGuids_When_Validate_Then_Valid(object? inputs)
    {
        var attr = new ContainsOnlyNonEmptyGuidsAttribute();
        Assert.True(attr.IsValid(inputs));
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
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

    private class ValidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] { null };
            yield return new object[] { new Guid[] { } };
            yield return new object[] { new Guid?[] { } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string?[] { } };
            yield return new object[] { new Guid[] { new("f8daff85-4393-42ae-9ab5-8620ab20c8da") } };
            yield return new object[] { new string[] { "d78b48f9-37b8-47dd-8e47-0325dd3e7899" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    private class InvalidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object[] { new Guid[] { default } };
            yield return new object[] { new Guid?[] { default } };
            yield return new object[] { new string[] { string.Empty } };
            yield return new object[] { new string?[] { default } };
            yield return new object[] { new Guid[] { Guid.Empty } };
            yield return new object[] { new string[] { "00000000-0000-0000-0000-000000000000" } };
            yield return new object[] { new int[] { 0 } };
            yield return new object[] { new Guid[] { default, new("846398d2-416d-4f05-86b0-431e11117abc") } };
            yield return new object[] { new Guid[] { Guid.Empty, new("08bfec4a-da43-40de-a15a-0d10f227e6b7") } };
            yield return new object[] { new Guid?[] { default, new Guid("2dd81281-7498-411c-aea1-5cec96f2dd8d") } };
            yield return new object[] { new string[] { "00000000-0000-0000-0000-000000000000", "7d8c61f7-a334-4599-b801-45f0934099a4" } };
            yield return new object[] { new string[] { string.Empty, "7c3cdfba-4466-44ab-bb95-da02eac67380" } };
            yield return new object[] { new string?[] { default, "f8daff85-4393-42ae-9ab5-8620ab20c8da" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    private class SomeClass
    {
        [ContainsOnlyNonEmptyGuids]
        public Guid[] Values { get; set; } = new[] { Guid.Empty };
    }
}