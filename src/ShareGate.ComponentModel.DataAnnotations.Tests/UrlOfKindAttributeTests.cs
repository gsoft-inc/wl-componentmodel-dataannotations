using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ShareGate.ComponentModel.DataAnnotations.Tests;

public class UrlOfKindAttributeTests
{
    [Theory]
    [InlineData(UriKind.Absolute)]
    [InlineData(UriKind.Relative)]
    [InlineData(UriKind.RelativeOrAbsolute)]
    public void IsValid_Returns_True_When_Value_Is_Null(UriKind uriKind)
    {
        var attr = new UrlOfKindAttribute(uriKind);
        Assert.True(attr.IsValid(null));
    }

    [Theory]
    [InlineData("foo/bar", UriKind.Relative)]
    [InlineData("foo/bar", UriKind.RelativeOrAbsolute)]
    [InlineData("https://foo.bar/", UriKind.Absolute)]
    [InlineData("https://foo.bar/", UriKind.RelativeOrAbsolute)]
    public void IsValid_Returns_True_When_Right_UriKind(string value, UriKind uriKind)
    {
        var attr = new UrlOfKindAttribute(uriKind);
        Assert.Equal(uriKind, attr.Kind);
        Assert.True(attr.IsValid(value));
        Assert.True(attr.IsValid(new Uri(value, uriKind)));
    }

    [Theory]
    [InlineData("foo/bar", UriKind.Absolute)]
    [InlineData("https://foo.bar/", UriKind.Relative)]
    public void IsValid_Returns_False_When_Wrong_UriKind(string value, UriKind uriKind)
    {
        var attr = new UrlOfKindAttribute(uriKind);
        Assert.Equal(uriKind, attr.Kind);
        Assert.False(attr.IsValid(value));
    }

    [Theory]
    [InlineData(UriKind.Absolute)]
    [InlineData(UriKind.Relative)]
    [InlineData(UriKind.RelativeOrAbsolute)]
    public void IsValid_Returns_False_When_Value_Is_Not_A_String(UriKind uriKind)
    {
        var invalidValue = new object();
        var attr = new UrlOfKindAttribute(uriKind);
        Assert.False(attr.IsValid(invalidValue));
    }

    [Fact]
    public void Validator_TryValidateObject_Returns_The_Expected_Error_Messages_When_Validation_Fails()
    {
        var something = new Something();
        var expectedValue2ErrorMessage = UrlOfKindAttribute.ErrorMessageFormat.FormatInvariant(UrlOfKindAttribute.RelativeKind).FormatInvariant(nameof(Something.Value2));
        var expectedValue3ErrorMessage = UrlOfKindAttribute.ErrorMessageFormat.FormatInvariant(UrlOfKindAttribute.AbsoluteKind).FormatInvariant(nameof(Something.Value3));

        var results = new List<ValidationResult>();
        var context = new ValidationContext(something, serviceProvider: null, items: null);
        var isValid = Validator.TryValidateObject(something, context, results, validateAllProperties: true);

        Assert.False(isValid);
        Assert.Equal(2, results.Count);
        Assert.Single(results, x => x.ErrorMessage == expectedValue2ErrorMessage);
        Assert.Single(results, x => x.ErrorMessage == expectedValue3ErrorMessage);
    }

    private class Something
    {
        [UrlOfKind(UriKind.RelativeOrAbsolute)]
        public string Value1 => ":@ctu@lly_w0rks:#||https://foo";

        [UrlOfKind(UriKind.Relative)]
        public string Value2 => "https://foo.bar";

        [UrlOfKind(UriKind.Absolute)]
        public string Value3 => "foo/bar";
    }
}