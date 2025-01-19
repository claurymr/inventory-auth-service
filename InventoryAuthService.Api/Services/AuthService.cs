using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryAuthService.Api.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace InventoryAuthService.Api.Services;
/// <summary>
/// AuthService class that implements the IAuthService interface.
/// Provides functionality to generate JWT tokens.
/// </summary>
/// <param name="authSettings">The authentication settings used for generating tokens.</param>
public class AuthService(AuthSettings authSettings) : IAuthService
{
    private readonly AuthSettings _authSettings = authSettings;

    /// <inheritdoc/>
    public async Task<TokenResponse> GenerateToken(string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_authSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, _authSettings.Role)
                ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _authSettings.Issuer,
            Audience = _authSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return await Task.FromResult(TokenResponse.Success(tokenString));
    }
}