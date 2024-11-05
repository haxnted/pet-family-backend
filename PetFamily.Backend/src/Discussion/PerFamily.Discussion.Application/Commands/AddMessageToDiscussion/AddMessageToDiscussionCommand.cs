using PetFamily.Core.Abstractions;

namespace PerFamily.Discussion.Application.Commands.AddMessageToDiscussion;

public record AddMessageToDiscussionCommand(Guid DiscussionId, Guid UserId, string Message) : ICommand;
