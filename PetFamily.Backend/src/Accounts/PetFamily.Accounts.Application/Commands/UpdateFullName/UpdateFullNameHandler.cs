using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.UpdateFullName;

public class UpdateFullNameHandler(
    UserManager<User> userManager,
    IValidator<UpdateFullNameCommand> validator) : ICommandHandler<UpdateFullNameCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        UpdateFullNameCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);
        if (user is null)
            return Errors.General.NotFound(command.UserId).ToErrorList();

        var fullName = FullName.Create(command.FullNameDto.Name, 
            command.FullNameDto.Surname,
            command.FullNameDto.Patronymic).Value;

        user.FullName = fullName;
        await userManager.UpdateAsync(user);
        
        return UnitResult.Success<ErrorList>();
    }
}
