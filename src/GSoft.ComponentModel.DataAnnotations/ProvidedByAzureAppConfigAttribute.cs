using System;

namespace GSoft.ComponentModel.DataAnnotations;

/// <summary>
/// This attribute has no effect. Its purpose is to let developers know that a configuration property
/// might be loaded from Azure App Configuration.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ProvidedByAzureAppConfigAttribute : Attribute
{
}