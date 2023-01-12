using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GSoft.ComponentModel.DataAnnotations.Tests;

public class GuidAttributeTests
{
    private static readonly IEnumerable<string> ValidGuidFormats = new HashSet<string>
    {
        "N", "D", "B", "P", "X",
    };

    [Fact]
    public void IsValid_Returns_True_When_Value_Is_Null()
    {
        var attr = new GuidAttribute();
        Assert.True(attr.IsValid(null));
    }

    [Theory]
    [InlineData("f4aebf097ac845219b0b918d8139bde0")]
    [InlineData("f4aebf09-7ac8-4521-9b0b-918d8139bde0")]
    [InlineData("{f4aebf09-7ac8-4521-9b0b-918d8139bde0}")]
    [InlineData("(f4aebf09-7ac8-4521-9b0b-918d8139bde0)")]
    [InlineData("{0xf4aebf09,0x7ac8,0x4521,{0x9b,0x0b,0x91,0x8d,0x81,0x39,0xbd,0xe0}}")]
    public void IsValid_Returns_True_When_Valid_Guid_String_Without_Format(string value)
    {
        var format = default(string);
        var attr = new GuidAttribute();
        Assert.Null(attr.Format);
        Assert.True(attr.IsValid(value));
    }

    [Theory]
    [InlineData("f4aebf097ac845219b0b918d8139bde0", "N")]
    [InlineData("f4aebf09-7ac8-4521-9b0b-918d8139bde0", "D")]
    [InlineData("{f4aebf09-7ac8-4521-9b0b-918d8139bde0}", "B")]
    [InlineData("(f4aebf09-7ac8-4521-9b0b-918d8139bde0)", "P")]
    [InlineData("{0xf4aebf09,0x7ac8,0x4521,{0x9b,0x0b,0x91,0x8d,0x81,0x39,0xbd,0xe0}}", "X")]
    public void IsValid_Returns_True_When_Valid_Guid_String_With_Matching_Format(string value, string valueFormat)
    {
        var attr = new GuidAttribute(valueFormat);
        Assert.Equal(valueFormat, attr.Format);
        Assert.True(attr.IsValid(value));
    }

    [Theory]
    [InlineData("f4aebf097ac845219b0b918d8139bde0", "N")]
    [InlineData("f4aebf09-7ac8-4521-9b0b-918d8139bde0", "D")]
    [InlineData("{f4aebf09-7ac8-4521-9b0b-918d8139bde0}", "B")]
    [InlineData("(f4aebf09-7ac8-4521-9b0b-918d8139bde0)", "P")]
    [InlineData("{0xf4aebf09,0x7ac8,0x4521,{0x9b,0x0b,0x91,0x8d,0x81,0x39,0xbd,0xe0}}", "X")]
    public void IsValid_Returns_False_When_Valid_Guid_String_With_Non_Matching_Format(string value, string valueFormat)
    {
        foreach (var format in ValidGuidFormats)
        {
            if (format != valueFormat)
            {
                var attr = new GuidAttribute(format);
                Assert.False(attr.IsValid(value));
            }
        }
    }

    [Fact]
    public void IsValid_Returns_False_When_Valid_Guid_String_With_Invalid_Format()
    {
        const string invalidFormat = "?";
        var attr = new GuidAttribute(invalidFormat);
        Assert.False(attr.IsValid("f4aebf097ac845219b0b918d8139bde0"));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Invalid_Guid_String()
    {
        const string invalidStringValue = "foobar";
        var attr = new GuidAttribute();
        Assert.False(attr.IsValid(invalidStringValue));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_Not_A_String()
    {
        var invalidValue = new object();
        var attr = new GuidAttribute();
        Assert.False(attr.IsValid(invalidValue));
    }

    [Fact]
    public void Validator_TryValidateObject_Returns_The_Expected_Error_Messages_When_Validation_Fails()
    {
        var something = new Something();
        var expectedValue1ErrorMessage = GuidAttribute.ErrorMessageWithoutFormatFormat.FormatInvariant(nameof(Something.Value1));
        var expectedValue2ErrorMessage = GuidAttribute.ErrorMessageWithFormatFormat.FormatInvariant("D").FormatInvariant(nameof(Something.Value2));

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
        [Guid]
        public string Value1 => "not_a_valid_guid_string";

        [Guid("D")]
        public string Value2 => "f4aebf097ac845219b0b918d8139bde0";
    }
}