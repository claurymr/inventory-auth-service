using InventoryAuthService.Api.Contracts;

namespace InventoryAuthService.Api.Services;
/// <summary>
/// Defines the contract for authentication services.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Generates an authentication token for the specified user.
    /// </summary>
    /// <param name="userName">The username for which to generate the token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the generated token response.</returns>
    Task<TokenResponse> GenerateToken(string userName);
}