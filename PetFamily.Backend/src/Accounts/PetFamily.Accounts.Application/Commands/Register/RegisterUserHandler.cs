using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Framework;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.Register;

public class RegisterUserHandler(
    IAccountsUnitOfWork unitOfWork,
    IParticipantAccountManager accountManager,
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IValidator<RegisterUserCommand> validator,
    ILogger<RegisterUserHandler> logger) : ICommandHandler<RegisterUserCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var existsUserWithUserName = await userManager.FindByNameAsync(command.UserName);
        if (existsUserWithUserName != null)
            return Errors.General.AlreadyExist("username").ToErrorList();

        var role = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Participant, cancellationToken);
        if (role is null)
            return Errors.General.NotFound().ToErrorList();

        var transaction = await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var user = User.CreateParticipant(command.UserName, command.Email, role);

            var result = await userManager.CreateAsync(user, command.Password);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return new ErrorList(errors);
            }

            var fullname = FullName.Create(command.Name, command.Surname, command.Patronymic).Value;
            var participantAccount = new ParticipantAccount(fullname, user);
            await accountManager.CreateParticipantAccountAsync(participantAccount, cancellationToken);

            transaction.Commit();
            await unitOfWork.SaveChanges(cancellationToken);
            logger.LogInformation("User {UserName} has created a new account", command.UserName);
            
            return UnitResult.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            logger.Log(LogLevel.Critical, "Error during user registration {ex}", ex);
            return Error.Failure("Error.during.user.registration", "Error during user registration").ToErrorList();
        }
    }
}
