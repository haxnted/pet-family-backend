using System.Security.Claims;
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Accounts.Contracts.Responses;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Framework.Authorization;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application.Commands.RefreshToken;

public class RefreshTokenHandler(
    IValidator<RefreshTokenCommand> validator,
    ITokenProvider tokenProvider,
    IRefreshSessionManager refreshSessionManager) : ICommandHandler<LoginResponse, RefreshTokenCommand>
{
    public async Task<Result<LoginResponse, ErrorList>> Execute(
        RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var refreshSession = await GetRefreshSession(command, cancellationToken);
        if (refreshSession.IsFailure)
            return refreshSession.Error.ToErrorList();

        var userClaims = await tokenProvider.GetUserClaims(command.AccessToken);
        if (userClaims.IsFailure)
            return userClaims.Error.ToErrorList();

        var userId = GetUserId(refreshSession.Value, userClaims.Value);
        if (userId.IsFailure)  
            return userId.Error.ToErrorList();

        var jti = GetRefreshTokenJti(refreshSession.Value, userClaims.Value);
        if (jti.IsFailure)
            return jti.Error.ToErrorList();

        refreshSessionManager.Delete(refreshSession.Value);
        var user = refreshSession.Value.User;

        var accessToken = tokenProvider.GenerateAccessToken(user);
        var refreshToken = await tokenProvider.GenerateRefreshToken(user, jti.Value, cancellationToken);

        return new LoginResponse(accessToken.AccessToken, refreshToken);
    }

    private async Task<Result<RefreshSession, Error>> GetRefreshSession(
        RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshSession
            = await refreshSessionManager.GetByRefreshTokenAsync(command.RefreshToken, cancellationToken);

        if (refreshSession.IsFailure)
            return Errors.General.NotFound();

        if (refreshSession.Value.ExpiredAt < DateTime.UtcNow)
            return Errors.Token.ExpiredToken();

        return refreshSession;
    }

    private Result<Guid, Error> GetUserId(RefreshSession refreshSession,
        IReadOnlyList<Claim> userClaims)
    {
        var userIdClaim = userClaims.FirstOrDefault(x => x.Type == CustomClaims.Id);
        if (userIdClaim is null)
            return Errors.User.InvalidCredentials();

        if (Guid.TryParse(userIdClaim.Value, out var userId) == false)
            return Errors.User.InvalidCredentials();

        if (refreshSession.UserId != userId)
            return Errors.Token.InvalidToken();
        
        return userId;
    }
    private Result<Guid, Error> GetRefreshTokenJti(RefreshSession refreshSession,
        IReadOnlyList<Claim> userClaims)
    {
        var userJtiClaim = userClaims.FirstOrDefault(x => x.Type == CustomClaims.Jti);
        if (userJtiClaim is null)
            return Errors.User.InvalidCredentials();

        if (Guid.TryParse(userJtiClaim.Value, out var userJti) == false)
            return Errors.User.InvalidCredentials();

        if (userJti != refreshSession.Jti)
            return Errors.User.InvalidCredentials();

        return userJti;
    }
}