namespace InventoryAuthService.Api.Contracts;
/// <summary>
/// Represents the authentication settings required for the inventory authentication service.
/// </summary>
public record AuthSettings
{
    /// <summary>
    /// Gets the username required for authentication.
    /// </summary>
    public string UserName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the password required for authentication.
    /// </summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Gets the role associated with the user.
    /// </summary>
    public string Role { get; init; } = string.Empty;

    /// <summary>
    /// Gets the secret key used for token generation.
    /// </summary>
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// Gets the issuer of the token.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// Gets the audience for which the token is intended.
    /// </summary>
    public string Audience { get; init; } = string.Empty;
}