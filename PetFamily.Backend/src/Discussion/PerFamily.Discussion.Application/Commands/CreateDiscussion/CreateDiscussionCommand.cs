using PetFamily.Core.Abstractions;

namespace PerFamily.Discussion.Application.Commands.CreateDiscussion;

public record CreateDiscussionCommand(Guid RelationId, Guid UserId, Guid AdminId) : ICommand;
