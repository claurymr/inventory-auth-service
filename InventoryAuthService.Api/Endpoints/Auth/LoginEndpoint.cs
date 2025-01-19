using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using InventoryAuthService.Api.Contracts;
using InventoryAuthService.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryAuthService.Api.Endpoints.Auth;
/// <summary>
/// Endpoint for handling user login requests in the Inventory Management System.
/// </summary>
/// <param name="authSettings">The authentication settings containing valid username and password.</param>
/// <param name="authService">The authentication service responsible for generating tokens.</param>
/// <param name="validator">The validator for validating login requests.</param>
/// <response code="200">Returns a token response if the login is successful.</response>
/// <response code="400">Returns a validation result if the login request is invalid.</response>
/// <response code="403">Returns a forbid result if the login credentials are incorrect.</response>\
public class CreateProductEndpoint(AuthSettings authSettings, IAuthService authService, IValidator<LoginRequest> validator)
    : Endpoint<LoginRequest, Results<Ok<TokenResponse>, BadRequest<ValidationResult>, ForbidHttpResult>>
{
    private readonly IAuthService _authService = authService;
    private readonly AuthSettings _authSettings = authSettings;
    private readonly IValidator<LoginRequest> _validator = validator;

    public override void Configure()
    {
        Verbs(Http.POST);
        Post("/login");

        Options(x =>
        {
            x.AllowAnonymous();
            x.WithDisplayName("Login to Inventory Management System");
            x.Produces<Ok<TokenResponse>>();
            x.Produces<BadRequest>();
            x.Produces<ForbidHttpResult>();
            x.WithOpenApi();
        });
    }

    public override async Task<Results<Ok<TokenResponse>, BadRequest<ValidationResult>, ForbidHttpResult>> ExecuteAsync(LoginRequest req, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(req, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult);
        }

        if (req.UserName != _authSettings.UserName || req.Password != _authSettings.Password)
        {
            return TypedResults.Forbid();
        }

        var token = await _authService.GenerateToken(req.UserName);
        return TypedResults.Ok(token);
    }
}