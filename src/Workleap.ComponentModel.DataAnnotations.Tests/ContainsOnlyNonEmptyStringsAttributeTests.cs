using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public sealed class ContainsOnlyNonEmptyStringsAttributeTests
{
    [Theory]
    [ClassData(typeof(ValidData))]
    public void Given_ValidStrings_When_Validate_Then_Valid(object? inputs)
    {
        var attr = new ContainsOnlyNonEmptyStringsAttribute();
        Assert.True(attr.IsValid(inputs));
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
    public void Given_InvalidGuids_When_Validate_Then_Invalid(object? inputs)
    {
        var attr = new ContainsOnlyNonEmptyStringsAttribute();
        var result = attr.IsValid(inputs);
        Assert.False(result);
    }

    [Fact]
    public void Validator_TryValidateObject_Returns_The_Expected_Error_Message_When_Validation_Fails()
    {
        var something = new SomeClass();
        var expectedErrorMessage = string.Format(CultureInfo.InvariantCulture, ContainsOnlyNonEmptyStringsAttribute.ErrorMessageFormat, nameof(SomeClass.Values));

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
            yield return new object[] { new string?[] { } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "Lorem ipsum dolor sit amet" } };
            yield return new object[] { new string[] { "Lorem ipsum dolor sit amet", "Etiam porta velit non nisi feugiat pulvinar" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    private class InvalidData : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object[] { new string?[] { string.Empty, default } };
            yield return new object[] { new string?[] { string.Empty } };
            yield return new object[] { new string?[] { default } };
            yield return new object[] { new string[] { " " } };
            yield return new object[] { new string[] { string.Empty, "Lorem ipsum dolor sit amet" } };
            yield return new object[] { new string?[] { default, "Lorem ipsum dolor sit amet" } };
            yield return new object[] { new string[] { "Lorem ipsum dolor sit amet", " " } };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    private class SomeClass
    {
        [ContainsOnlyNonEmptyStrings]
        public string?[] Values { get; set; } = new[] { string.Empty };
    }
}