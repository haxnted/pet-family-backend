using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PerFamily.Discussion.Application;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Infrastructure.Repositories;

public class DiscussionRepository(DiscussionWriteDbContext context) : IDiscussionRepository
{
    public async Task Add(Domain.Discussion discussion, CancellationToken cancellationToken = default)
    {
        await context.Discussions.AddAsync(discussion, cancellationToken);
    }

    public void Save(Domain.Discussion discussion)
    {
        context.Discussions.Attach(discussion);
    }

    public async Task<Result<Domain.Discussion, Error>> GetById(Guid discussionId, CancellationToken cancellationToken = default)
    {
        var discussion = await context.Discussions
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.Id == discussionId, cancellationToken: cancellationToken);

        if (discussion is null)
            return Errors.General.NotFound(discussionId);

        return discussion;
    }

    public async Task<Result<Domain.Discussion, Error>> GetByRelationId(Guid relationId, CancellationToken cancellationToken = default)
    {
        var discussion = await context.Discussions
            .Include(d => d.Messages)
            .FirstOrDefaultAsync(d => d.RelationId == relationId, cancellationToken: cancellationToken);

        if (discussion is null)
            return Errors.General.NotFound(relationId);

        return discussion;
    }
}
