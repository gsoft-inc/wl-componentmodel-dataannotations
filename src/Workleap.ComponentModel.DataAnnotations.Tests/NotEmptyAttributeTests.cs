using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public sealed class NotEmptyAttributeTests
{
    [Fact]
    public void IsValid_Returns_True_When_Value_Is_Null()
    {
        var attr = new NotEmptyAttribute();
        Assert.True(attr.IsValid(null));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Object()
        => AssertIsNotValid(new object());

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Empty_Generic_Array()
        => AssertIsNotValid(Array.Empty<object>());

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Empty_Generic_List()
        => AssertIsNotValid(new List<object>());

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Empty_Generic_HashSet()
        => AssertIsNotValid(new HashSet<object>());

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Empty_ArrayList()
        => AssertIsNotValid(new ArrayList());

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_An_Empty_Yielded_Enumerable()
        => AssertIsNotValid(EmptyObjectEnumeration());

    [Fact]
    public void IsValid_Returns_True_When_Value_Is_A_Generic_Array_With_One_Element()
        => AssertIsValid(new[] { 1 });

    [Fact]
    public void IsValid_Returns_True_When_Value_Is_A_Generic_List_With_One_Element()
        => AssertIsValid(new List<int> { 1 });

    [Fact]
    public void IsValid_Returns_True_When_Value_Is_A_Generic_HashSet_With_One_Element()
        => AssertIsValid(new HashSet<int> { 1 });

    [Fact]
    public void IsValid_Returns_True_When_Value_Is_A_ArrayList_With_One_Element()
        => AssertIsValid(new ArrayList { 1 });

    [Fact]
    public void IsValid_Returns_True_When_Value_Is_An_Yielded_Enumerable_With_At_Least_One_Element()
        => AssertIsValid(SingleObjectEnumeration());

    private static IEnumerable<object> EmptyObjectEnumeration()
    {
        yield break;
    }

    private static IEnumerable<object> SingleObjectEnumeration()
    {
        yield return new object();
        throw new InvalidOperationException(nameof(NotEmptyAttribute) + " is not supposed to consume more than one element from an enumerable");
    }

    private static void AssertIsValid(object value) => Assert.True(new NotEmptyAttribute().IsValid(value));

    private static void AssertIsNotValid(object value) => Assert.False(new NotEmptyAttribute().IsValid(value));

    [Fact]
    public void Validator_TryValidateObject_Returns_The_Expected_Error_Message_When_Validation_Fails()
    {
        var something = new Something();
        var expectedErrorMessage = string.Format(CultureInfo.InvariantCulture, NotEmptyAttribute.ErrorMessageFormat, nameof(Something.Values));

        var results = new List<ValidationResult>();
        var context = new ValidationContext(something, serviceProvider: null, items: null);
        var isValid = Validator.TryValidateObject(something, context, results, validateAllProperties: true);

        Assert.False(isValid);
        var result = Assert.Single(results);
        Assert.NotNull(result.ErrorMessage);
        Assert.Equal(expectedErrorMessage, result.ErrorMessage);
    }

    private class Something
    {
        [NotEmpty]
        public string[] Values { get; } = Array.Empty<string>();
    }
}