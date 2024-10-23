using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Application.Commands.Login;
using PetFamily.Accounts.Application.Commands.Register;
using PetFamily.Accounts.Application.Commands.UpdateAccountRequisites;
using PetFamily.Accounts.Application.Commands.UpdateAccountSocialLinks;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Accounts.Presentation.Requests;
using PetFamily.Core.Dto;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Presentation;

public class AccountsController : ApplicationController
{
    //test
    [Permission("species.create")]
    [HttpPost("admin")]
    public IActionResult CreatePet()
    {
        return Ok();
    }

    //test
    [Permission("volunteer.create")]
    [HttpPost("user")]
    public IActionResult DeletePet()
    {
        return Ok();
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.User.UpdateSocialLinks)]
    [HttpPatch("{userId:guid}/social-links")]
    public async Task<IActionResult> UpdateAccountSocialLinks(
        [FromBody] IEnumerable<SocialLinkDto> request,
        [FromRoute] Guid userId,
        [FromServices] UpdateAccountSocialLinksHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(new UpdateAccountSocialLinksCommand(userId, request), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }


    [Permission(Permissions.Volunteer.UpdateRequisites)]
    [HttpPatch("{userId:guid}/requisites")]
    public async Task<IActionResult> UpdateVolunteerRequisites(
        [FromBody] IEnumerable<RequisiteDto> request,
        [FromRoute] Guid userId,
        [FromServices] UpdateAccountRequisitesHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(new UpdateAccountRequisitesCommand(userId, request), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    //test volunteer methods
    [HttpPost("registration-volunteer")]
    public async Task<IActionResult> CreateVolunteer(
        IAccountsUnitOfWork unitOfWork,
        IVolunteerAccountManager accountManager,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand("test",
            "test2",
            "test3",
            "volunteerTest",
            "volunteerTest@gmail.com",
            "zxcVolunteer001");

        var existsUserWithUserName = await userManager.FindByNameAsync(command.UserName);
        if (existsUserWithUserName != null)
            return BadRequest("уже есть але");

        var role = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Volunteer, cancellationToken);
        if (role is null)
            return BadRequest("уже есть але");

        var user = Domain.User.CreateParticipant(command.UserName, command.Email, role);

        var result = await userManager.CreateAsync(user, command.Password);
        if (result.Succeeded == false)
        {
            return BadRequest("уже есть але");
        }

        var fullname = FullName.Create(command.Name, command.Surname, command.Patronymic).Value;
        var participantAccount = new VolunteerAccount(fullname, 5, [], user);
        await accountManager.CreateVolunteerAccountAsync(participantAccount, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        return Ok();
    }

    //test
    [HttpGet("volunteer-jwt")]
    public async Task<IActionResult> GetVolunteerJwt(
        ITokenProvider tokenProvider,
        UserManager<User> userManager,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand("volunteerTest@gmail.com", "zxcVolunteer001");
        var existsUser = await userManager.FindByEmailAsync(command.Email);
        if (existsUser is null)
            return BadRequest();

        var passwordCorrect = await userManager.CheckPasswordAsync(existsUser, command.Password);
        if (!passwordCorrect)
            return BadRequest();

        var token = tokenProvider.GenerateAccessToken(existsUser, cancellationToken);
        return Ok(token);
    }
}
