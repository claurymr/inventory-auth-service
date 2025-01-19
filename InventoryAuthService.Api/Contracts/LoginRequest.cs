namespace InventoryAuthService.Api.Contracts;

/// <summary>
/// Represents a request to log in to the system.
/// </summary>
public record LoginRequest
{
    /// <summary>
    /// Gets the username of the user attempting to log in.
    /// </summary>
    public string UserName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the password of the user attempting to log in.
    /// </summary>
    public string Password { get; init; } = string.Empty;
}