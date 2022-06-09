using System;

namespace ShareGate.ComponentModel.DataAnnotations;

/// <summary>
/// This attribute has no effect. Its purpose is to let developers know that a configuration property
/// might be loaded from Azure Key Vault.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ProvidedByAzureKeyVaultAttribute : Attribute
{
}