# ShareGate.ComponentModel.DataAnnotations

Provides multiple new data annotation attributes, such as `[Guid]`, `[NotEmpty]`, `[ValidateProperties]`.

[![nuget](https://img.shields.io/nuget/v/ShareGate.ComponentModel.DataAnnotations.svg?logo=nuget)](https://www.nuget.org/packages/ShareGate.ComponentModel.DataAnnotations/)
[![build](https://img.shields.io/github/workflow/status/gsoft-inc/sg-componentmodel-dataannotations/CI%20build?logo=github)](https://github.com/gsoft-inc/sg-componentmodel-dataannotations/actions/workflows/ci.yml)


## Getting started

```
dotnet add package ShareGate.ComponentModel.DataAnnotations
```

Decorate your properties, fields and method parameters with these new data annotation attributes.

Most of them are validation attributes that can be used in model validation. They work the same way as built-in .NET data annotations such as `[Required]`, `[Range]`, `[RegularExpression]`, etc.

The most useful validation attribute here is probably `ValidatePropertiesAttribute`, as it validates all the properties of an object. This allows in-depth, nested validation of an entire complex object graph.

## List of data annotation attributes

| Attribute | Description |
| ------------- | ------------- |
| `GuidAttribute` | Validates that a string property is a well-formatted GUID with an optional format |
| `NotEmptyAttribute` | Validate that an enumerable property is not empty |
| `ValidatePropertiesAttribute` | Validates **all properties of a complex type property** (nested object validation) |
| `TimeSpanAttribute` | Validates that a string property is a well-formatted TimeSpan with an optional format |
| `UrlOfKindAttribute` | Validates that a string or `Uri` property is a well-formatted url of the specified `UriKind` |
| `ContainsAttribute` | Validates that a string contains the specified substring (casing can be specified) |
| `StartsWithAttribute` | Validates that a string starts with the specified substring (casing can be specified) |
| `EndsWithAttribute` | Validates that a string ends with the specified substring (casing can be specified) |
| `ProvidedByAzureKeyVaultAttribute` | Indicates that a property value might be loaded from Azure Key Vault (_has no effect_) |
| `SensitiveInformationAttribute` | Indicates that a property contains sensitive information, such as personally identifiable information (PII), or any other information that might result in loss of an advantage or level of security if disclosed to others (_has no effect_) |
| `NonSensitiveInformationAttribute` | Indicates that a property does not contain sensitive information (_has no effect_) |

## 🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change. If you're interested, definitely check out our Contributing Guide!


## License

Copyright © 2022, GSoft inc. This code is licensed under the Apache License, Version 2.0. You may obtain a copy of this license at https://github.com/gsoft-inc/sharegate-license/blob/master/LICENSE.
