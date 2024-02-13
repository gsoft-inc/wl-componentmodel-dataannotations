using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public class GuidAttributeTests
{
    private const string ACustomErrorMessage = "A custom error message.";

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
        var expectedValue1ErrorMessage = GuidAttribute.ErrorMessageWithoutFormatFormat.FormatInvariant(nameof(Something.Value1), string.Empty);
        var expectedValue2ErrorMessage = GuidAttribute.ErrorMessageWithFormatFormat.FormatInvariant(nameof(Something.Value2), string.Empty, "D");
        var expectedValue3ErrorMessage = GuidAttribute.ErrorMessageWithoutFormatFormat.FormatInvariant(nameof(Something.Value3), GuidAttribute.ErrorMessageNonEmptyPart);
        var expectedValue4ErrorMessage = GuidAttribute.ErrorMessageWithoutFormatFormat.FormatInvariant(nameof(Something.Value4), GuidAttribute.ErrorMessageNonEmptyPart);
        var expectedValue5ErrorMessage = ACustomErrorMessage;

        var results = new List<ValidationResult>();
        var context = new ValidationContext(something, serviceProvider: null, items: null);
        var isValid = Validator.TryValidateObject(something, context, results, validateAllProperties: true);

        Assert.False(isValid);
        Assert.Equal(5, results.Count);

        var errorMessages = results.Select(x => x.ErrorMessage).ToArray();
        Assert.Single(errorMessages, expectedValue1ErrorMessage);
        Assert.Single(errorMessages, expectedValue2ErrorMessage);
        Assert.Single(errorMessages, expectedValue3ErrorMessage);
        Assert.Single(errorMessages, expectedValue4ErrorMessage);
        Assert.Single(errorMessages, expectedValue5ErrorMessage);
    }

    private class Something
    {
        [Guid]
        public string Value1 => "not_a_valid_guid_string";

        [Guid("D")]
        public string Value2 => "f4aebf097ac845219b0b918d8139bde0";

        [Guid(AllowEmpty = false)]
        public string Value3 => "00000000-0000-0000-0000-000000000000";

        [Guid(AllowEmpty = false)]
        public Guid Value4 => Guid.Empty;

        [Guid(ErrorMessage = ACustomErrorMessage)]
        public string Value5 => "not_a_valid_guid_string";

        [Guid]
        public Guid Value6 => Guid.NewGuid();

        [Guid]
        public Guid Value7 => Guid.Empty;
    }
}