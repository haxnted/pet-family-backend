using CSharpFunctionalExtensions;
using PetFamily.Core.Dto.Discussions;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Contracts;

public interface IDiscussionContract
{
    public Task<Result<Guid, ErrorList>> CreateDiscussion(Guid RelationId, Guid UserId, Guid AdminId);
    public Task<UnitResult<ErrorList>> CloseDiscussionById(Guid DiscussionId, Guid AdminId);
    public Task<UnitResult<ErrorList>> AddMessage(Guid DiscussionId, Guid UserId, string Message);
}