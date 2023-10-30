namespace FamilySync.Core.Authentication.Enums;

public enum AccessLevel : int
{
    /// <summary>
    /// No access
    /// </summary>
    NONE = 0,

    /// <summary>
    /// Read only access
    /// </summary>
    READONLY = 1,

    /// <summary>
    /// Read / Write access for self to basic features.
    /// Inherits access from <see cref="AccessLevel.READONLY"/>
    /// </summary>
    USER_BASIC = 2,

    /// <summary>
    /// Read / Write access for self to premium features.
    /// Inherits access from <see cref="AccessLevel.USER_BASIC"/>
    /// </summary>
    USER_PREMIUM = 3,

    /// <summary>
    /// Full access to administrative features.
    /// Inherits access from <see cref="AccessLevel.USER_PREMIUM"/>
    /// </summary>
    ADMIN = 4
}