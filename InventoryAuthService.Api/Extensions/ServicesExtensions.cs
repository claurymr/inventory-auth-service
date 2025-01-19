using System.Security.Claims;
using System.Text;
using InventoryAuthService.Api.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InventoryAuthService.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring authentication and authorization services.
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// Adds authentication and authorization services to the specified <see cref="IServiceCollection"/>.
    /// Configures JWT bearer authentication using settings from the provided <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing authentication settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var secret = Encoding.UTF8.GetBytes(configuration["Auth:Secret"]!);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Auth:Issuer"],
                    ValidAudience = configuration["Auth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    RoleClaimType = ClaimTypes.Role
                };
                options.Authority = configuration["Auth:Issuer"];
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy => policy.RequireRole("admin"))
            .AddPolicy("AdminOrUser", policy => policy.RequireRole("admin", "user"));

        return services;
    }

    /// <summary>
    /// Adds configuration settings to the specified <see cref="IServiceCollection"/>.
    /// Configures the <see cref="AuthSettings"/> section from the provided <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing configuration settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddConfigSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthSettings>(configuration.GetSection("Auth"));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<AuthSettings>>().Value);
        return services;
    }
}