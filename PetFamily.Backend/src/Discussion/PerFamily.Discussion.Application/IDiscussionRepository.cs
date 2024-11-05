using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Application;

public interface IDiscussionRepository
{
    public Task Add(Domain.Discussion discussion, CancellationToken cancellationToken = default);

    public void Save(Domain.Discussion discussion);

    public Task<Result<Domain.Discussion, Error>> GetById(
        Guid discussionId, CancellationToken cancellationToken = default);
    public Task<Result<Domain.Discussion, Error>> GetByRelationId(
        Guid discussionId, CancellationToken cancellationToken = default);
}
