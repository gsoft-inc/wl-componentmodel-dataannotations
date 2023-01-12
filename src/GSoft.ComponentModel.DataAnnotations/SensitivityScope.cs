namespace GSoft.ComponentModel.DataAnnotations;

public enum SensitivityScope
{
    /// <summary>
    /// The sensitive information is scoped to a particular user.
    /// </summary>
    User,

    /// <summary>
    /// The sensitive information is scoped to a particular tenant or organization.
    /// </summary>
    Tenant,

    /// <summary>
    /// The sensitive information is scoped to a whole application.
    /// </summary>
    Application,
}