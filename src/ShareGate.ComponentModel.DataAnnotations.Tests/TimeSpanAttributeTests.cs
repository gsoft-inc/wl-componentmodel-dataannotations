using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ShareGate.ComponentModel.DataAnnotations.Tests;

public class TimeSpanAttributeTests
{
    [Fact]
    public void IsValid_Returns_True_When_Value_Is_Null()
    {
        var attr = new TimeSpanAttribute();
        Assert.True(attr.IsValid(null));
    }

    [Theory]
    [InlineData("23:28:03")]
    [InlineData("-23:28:03")]
    [InlineData("23:28:03.1195580")]
    [InlineData("-23:28:03.1195580")]
    [InlineData("0:23:28:03.1195580")]
    [InlineData("-0:23:28:03.1195580")]
    public void IsValid_Returns_True_When_Valid_TimeSpan_String_Without_Format(string value)
    {
        var format = default(string);
        var attr = new TimeSpanAttribute();
        Assert.Null(attr.Format);
        Assert.True(attr.IsValid(value));
    }

    [Theory]
    [InlineData("23:28:03", "c")]
    [InlineData("23:28:03", "t")]
    [InlineData("23:28:03", "T")]
    [InlineData("23:28:03", "g")]
    [InlineData("23:28:03.1195580", "c")]
    [InlineData("23:28:03.1195580", "t")]
    [InlineData("23:28:03.1195580", "T")]
    [InlineData("23:28:03.1195580", "g")]
    [InlineData("0:23:28:03.1195580", "g")]
    [InlineData("0:23:28:03.1195580", "G")]
    public void IsValid_Returns_True_When_Valid_TimeSpan_String_With_Matching_Format(string value, string valueFormat)
    {
        var attr = new TimeSpanAttribute(valueFormat);
        Assert.Equal(valueFormat, attr.Format);
        Assert.True(attr.IsValid(value));
    }

    [Theory]
    [InlineData("23:28:03", "G")]
    [InlineData("23:28:03.1195580", "G")]
    [InlineData("0:23:28:03.1195580", "c")]
    [InlineData("0:23:28:03.1195580", "t")]
    [InlineData("0:23:28:03.1195580", "T")]
    public void IsValid_Returns_False_When_Valid_TimeSpan_String_With_Non_Matching_Format(string value, string valueFormat)
    {
        var attr = new TimeSpanAttribute(valueFormat);
        Assert.False(attr.IsValid(value));
    }

    [Fact]
    public void IsValid_Returns_False_When_Valid_TimeSpan_String_With_Invalid_Format()
    {
        const string invalidFormat = "?";
        var attr = new TimeSpanAttribute(invalidFormat);
        Assert.False(attr.IsValid("23:28:03"));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Invalid_TimeSpan_String()
    {
        const string invalidStringValue = "foobar";
        var attr = new TimeSpanAttribute();
        Assert.False(attr.IsValid(invalidStringValue));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_Not_A_String()
    {
        var invalidValue = new object();
        var attr = new TimeSpanAttribute();
        Assert.False(attr.IsValid(invalidValue));
    }

    [Fact]
    public void Validator_TryValidateObject_Returns_The_Expected_Error_Messages_When_Validation_Fails()
    {
        var something = new Something();
        var expectedValue1ErrorMessage = TimeSpanAttribute.ErrorMessageWithoutFormatFormat.FormatInvariant(nameof(Something.Value1));
        var expectedValue2ErrorMessage = TimeSpanAttribute.ErrorMessageWithFormatFormat.FormatInvariant("G").FormatInvariant(nameof(Something.Value2));

        var results = new List<ValidationResult>();
        var context = new ValidationContext(something, serviceProvider: null, items: null);
        var isValid = Validator.TryValidateObject(something, context, results, validateAllProperties: true);

        Assert.False(isValid);
        Assert.Equal(2, results.Count);
        Assert.Single(results, x => x.ErrorMessage == expectedValue1ErrorMessage);
        Assert.Single(results, x => x.ErrorMessage == expectedValue2ErrorMessage);
    }

    private class Something
    {
        [TimeSpan]
        public string Value1 => "not_a_valid_timespan_string";

        [TimeSpan("G")]
        public string Value2 => "23:28:03";
    }
}