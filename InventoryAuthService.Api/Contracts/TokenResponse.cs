namespace InventoryAuthService.Api.Contracts;
/// <summary>
/// Represents a response containing a token or an error.
/// </summary>
public record TokenResponse
{
    /// <summary>
    /// Gets the token string.
    /// </summary>
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// Gets the error associated with the token response.
    /// </summary>
    public Exception Error { get; init; } = default!;

    /// <summary>
    /// Creates a failed token response with the specified error.
    /// </summary>
    /// <param name="error">The exception representing the error.</param>
    /// <returns>A <see cref="TokenResponse"/> instance with the specified error.</returns>
    public static TokenResponse Failed(Exception error)
    {
        return new TokenResponse { Error = error };
    }

    /// <summary>
    /// Creates a successful token response with the specified access token.
    /// </summary>
    /// <param name="accessToken">The access token string.</param>
    /// <returns>A <see cref="TokenResponse"/> instance with the specified access token.</returns>
    public static TokenResponse Success(string accessToken)
    {
        return new TokenResponse
        {
            Token = accessToken
        };
    }
}