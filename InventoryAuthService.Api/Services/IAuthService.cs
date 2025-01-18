using InventoryAuthService.Api.Contracts;

namespace InventoryAuthService.Api.Services;
public interface IAuthService
{
    Task<TokenResponse> GenerateToken(string userName);
}