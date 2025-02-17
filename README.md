# Workleap.ComponentModel.DataAnnotations

Provides multiple new data annotation attributes, such as `[Guid]`, `[NotEmpty]`, `[ValidateProperties]`.

[![nuget](https://img.shields.io/nuget/v/Workleap.ComponentModel.DataAnnotations.svg?logo=nuget)](https://www.nuget.org/packages/Workleap.ComponentModel.DataAnnotations/)
[![build](https://img.shields.io/github/actions/workflow/status/workleap/wl-componentmodel-dataannotations/publish.yml?logo=github)](https://github.com/workleap/wl-componentmodel-dataannotations/actions/workflows/publish.yml)


## Getting started

```
dotnet add package Workleap.ComponentModel.DataAnnotations
```

Decorate your properties, fields and method parameters with these new data annotation attributes.

Most of them are validation attributes that can be used in model validation. They work the same way as built-in .NET data annotations such as `[Required]`, `[Range]`, `[RegularExpression]`, etc.

The most useful validation attribute here is probably `ValidatePropertiesAttribute`, as it validates all the properties of an object. This allows in-depth, nested validation of an entire complex object graph.

## List of data annotation attributes

| Attribute                             | Description                                                                                                                                                                                                                                   |
|---------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GuidAttribute`                       | Validates that a string property is a well-formatted GUID with an optional format                                                                                                                                                             |
| `ContainsNonEmptyGuidAttribute`       | Validates that a Guid enumerable property contains at least one non-empty Guid                                                                                                                                                                                                                                              |
| `ContainsNonEmptyStringAttribute`     | Validates that a String enumerable property contains at least one non-empty String                                                                                                                                                           |
| `ContainsOnlyNonEmptyGuidsAttribute`  | Validates that a Guid enumerable property contains only non-empty Guids                                                                                                                                                                                                                                              |
| `ContainsOnlyNonEmptyStringsAttribute`| Validates that a String enumerable property contains only non-empty Strings                                                                                                                                                                                                                                             |
| `NotEmptyAttribute`                   | Validates that an enumerable property is not empty                                                                                                                                                                                            |
| `ValidatePropertiesAttribute`         | Validates **all properties of a complex type property** (nested object validation)                                                                                                                                                            |
| `TimeSpanAttribute`                   | Validates that a string property is a well-formatted TimeSpan with an optional format                                                                                                                                                         |
| `UrlOfKindAttribute`                  | Validates that a string or `Uri` property is a well-formatted url of the specified `UriKind`                                                                                                                                                  |
| `ContainsAttribute`                   | Validates that a string contains the specified substring (casing can be specified)                                                                                                                                                            |
| `StartsWithAttribute`                 | Validates that a string starts with the specified substring (casing can be specified)                                                                                                                                                         |
| `EndsWithAttribute`                   | Validates that a string ends with the specified substring (casing can be specified)                                                                                                                                                           |
| `ProvidedByAzureKeyVaultAttribute`    | Indicates that a property value might be loaded from Azure Key Vault (_has no effect_)                                                                                                                                                        |
| `ProvidedByAzureAppConfigAttribute`   | Indicates that a property value might be loaded from Azure App Configuration (_has no effect_)                                                                                                                                                |
| `SensitiveInformationAttribute`       | Indicates that a property contains sensitive information, such as personally identifiable information (PII), or any other information that might result in loss of an advantage or level of security if disclosed to others (_has no effect_) |
| `NonSensitiveInformationAttribute`    | Indicates that a property does not contain sensitive information (_has no effect_)                                                                                                                                                            |


## License

Copyright © 2022, Workleap. This code is licensed under the Apache License, Version 2.0. You may obtain a copy of this license at https://github.com/workleap/gsoft-license/blob/master/LICENSE.
