using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Application.Commands.CloseDiscussion;

public class CloseDiscussionHandler(
    IDiscussionRepository repository,
    IDiscussionUnitOfWork unitOfWork,
    ILogger<CloseDiscussionHandler> logger) : ICommandHandler<CloseDiscussionCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(CloseDiscussionCommand command, CancellationToken cancellationToken = default)
    {
        var discussion = await repository.GetById(command.DiscussionId, cancellationToken);
        if (discussion.IsFailure)
            return discussion.Error.ToErrorList();

        if (discussion.Value.Users.IsExisting(command.AdminId) == false)
            return Errors.General.AccessDenied("discussion").ToErrorList();
        
        discussion.Value.Close();
        await unitOfWork.SaveChanges(cancellationToken);
        
        logger.Log(LogLevel.Information, "Discussion with {DiscussionId} closed", discussion.Value.Id);
        
        return UnitResult.Success<ErrorList>();
    }
}

public record CloseDiscussionCommand(Guid DiscussionId, Guid AdminId) : ICommand;
