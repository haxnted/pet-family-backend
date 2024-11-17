using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Application.Commands.ApproveApplication;
using PetFamily.VolunteerRequest.Application.Commands.BanUser;
using PetFamily.VolunteerRequest.Application.Commands.CreateApplication;
using PetFamily.VolunteerRequest.Application.Commands.ExtendBanUser;
using PetFamily.VolunteerRequest.Application.Commands.RejectApplication;
using PetFamily.VolunteerRequest.Application.Commands.SendApplicationToRevision;
using PetFamily.VolunteerRequest.Application.Commands.TakeApplicationForReview;
using PetFamily.VolunteerRequest.Application.Commands.UnbanUser;
using PetFamily.VolunteerRequest.Application.Commands.UpdateApplication;
using PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;
using PetFamily.VolunteerRequest.Application.Queries.GetRemainingBanTime;
using PetFamily.VolunteerRequest.Application.Queries.GetRestrictionUsersWithPagination;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestById;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticularAdmin;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsWithPagination;
using PetFamily.VolunteerRequest.Presentation.Requests;

namespace PetFamily.VolunteerRequest.Presentation;

public class VolunteerRequestController(UserInfoRequest UserInfoRequest) : ApplicationController
{
    [HttpPatch("/application/{requestId}/review")]
    [Permission(Permissions.Admin.ReviewApplication)]
    public async Task<IActionResult> TakeForReview(
        [FromRoute] Guid requestId,
        [FromServices] TakeApplicationForReviewHandler handler,
        CancellationToken cancellationToken)
    {
        var adminId = UserInfoRequest.Id;
        var result = await handler.Execute(new TakeApplicationForReviewCommand(adminId, requestId), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPatch("/application/{requestId}/approval")]
    [Permission(Permissions.Admin.ApproveApplication)]
    public async Task<IActionResult> Approve(
        [FromRoute] Guid requestId,
        [FromServices] ApproveApplicationHandler handler,
        CancellationToken cancellationToken)
    {
        var adminId = UserInfoRequest.Id;
        var result = await handler.Execute(new ApproveApplicationCommand(adminId, requestId), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPatch("/application/{requestId}/revision")]
    [Permission(Permissions.Admin.SendToRevision)]
    public async Task<IActionResult> SendToRevision(
        [FromRoute] Guid requestId,
        [FromBody] string message,
        [FromServices] SendApplicationToRevisionHandler handler,
        CancellationToken cancellationToken)
    {
        var adminId = UserInfoRequest.Id;
        var result = await handler.Execute(new SendApplicationToRevisionCommand(adminId, requestId, message),
            cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPatch("/application/{requestId}/rejection")]
    [Permission(Permissions.Admin.RejectApplication)]
    public async Task<IActionResult> Reject(
        [FromRoute] Guid requestId,
        [FromBody] string message,
        [FromServices] RejectApplicationHandler handler,
        CancellationToken cancellationToken)
    {
        var adminId = UserInfoRequest.Id;
        var result = await handler.Execute(new RejectApplicationCommand(adminId, requestId, message),
            cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPatch("/application/{requestId}/renewal")]
    public async Task<IActionResult> UpdateApplication(
        [FromRoute] Guid requestId,
        [FromBody] VolunteerInformationDto volunteerInformationDto,
        [FromServices] UpdateApplicationHandler handler,
        CancellationToken cancellationToken)
    {
        var participantId = UserInfoRequest.Id;
        var result = await handler.Execute(
            new UpdateApplicationCommand(participantId, requestId, volunteerInformationDto),
            cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("user/{participantId:guid}/ban")]
    [Permission(Permissions.Admin.BanUser)]
    public async Task<IActionResult> BanUser(
        [FromRoute] Guid participantId,
        [FromBody] BanUserRequest request,
        [FromServices] BanUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(new BanUserCommand(participantId, request.CountDaysBanned, request.Message),
            cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpDelete("user/{participantId:guid}/unban")]
    [Permission(Permissions.Admin.UnbanUser)]
    public async Task<IActionResult> UnBanUser(
        [FromRoute] Guid participantId,
        [FromServices] UnbanUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(new UnbanUserCommand(participantId), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost]
    [Permission(Permissions.Participant.CreateApplication)]
    public async Task<IActionResult> CreateApplication(
        [FromBody] CreateApplicationRequest request,
        [FromServices] CreateApplicationHandler handler,
        CancellationToken cancellationToken)
    {
        var participantId = UserInfoRequest.Id;
        var result = await handler.Execute(request.ToCommand(participantId), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("user/extend-ban")]
    [Permission(Permissions.Admin.ExtendBanUser)]
    public async Task<IActionResult> ExtendBanUser(
        [FromBody] int countDays,
        [FromServices] ExtendBanUserHandler handler,
        CancellationToken cancellationToken)
    {
        var participantId = UserInfoRequest.Id;
        var result = await handler.Execute(new ExtendBanUserCommand(participantId, countDays), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpGet("restriction-users")]
    [Permission(Permissions.Admin.GetAllBanUsers)]
    public async Task<IActionResult> GetBannedUsers(
        [FromQuery] GetRestrictionUsersWithPaginationRequest request,
        [FromServices] GetRestrictionUsersWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(new GetRestrictionUsersWithPaginationQuery(request.Page, request.PageSize),
            cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("admin/available-applications")]
    [Permission(Permissions.Admin.GetVolunteerRequests)]
    public async Task<IActionResult> GetVolunteerRequestsWithPagination(
        [FromQuery] GetVolunteerRequestsWithPaginationRequest request,
        [FromServices] GetVolunteerRequestsWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Execute(new GetVolunteerRequestsWithPaginationQuery(request.SortBy,
                request.SortDirection,
                request.Page,
                request.PageSize),
            cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("{volunteerRequestId}")]
    [Permission(Permissions.User.GetVolunteerRequest)]
    public async Task<IActionResult> GetVolunteerRequest(
        [FromRoute] Guid volunteerRequestId,
        [FromServices] GetVolunteerRequestByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var participantId = UserInfoRequest.Id;
        var result = await handler.Execute(new GetVolunteerRequestByIdCommand(volunteerRequestId, participantId),
            cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    [Permission(Permissions.User.GetVolunteerRequests)]
    public async Task<IActionResult> GetVolunteerRequestsForParticipant(
        [FromQuery] GetVolunteerRequestsForParticipantRequest request,
        [FromServices] GetAllVolunteerRequestForParticipantHandler handler,
        CancellationToken cancellationToken)
    {
        var participantId = UserInfoRequest.Id;
        var result = await handler.Execute(request.ToQuery(participantId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("admin/applications")]
    [Permission(Permissions.Admin.GetVolunteerRequests)]
    public async Task<IActionResult> GetVolunteerRequestsForParticularAdmin(
        [FromQuery] GetVolunteerRequestsForParticularAdminRequest request,
        [FromServices] GetVolunteerRequestsForParticularAdminHandler handler,
        CancellationToken cancellationToken)
    {
        var adminId = UserInfoRequest.Id;
        var result = await handler.Execute(request.ToCommand(adminId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("remaining-ban-time")]
    [Permission(Permissions.Admin.GetVolunteerRequests)]
    public async Task<IActionResult> GetRemainingBanTime(
        [FromServices] GetRemainingBanTimeHandler handler,
        CancellationToken cancellationToken)
    {
        var userId = UserInfoRequest.Id;
        var result = await handler.Execute(new GetRemainingBanTimeQuery(userId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
