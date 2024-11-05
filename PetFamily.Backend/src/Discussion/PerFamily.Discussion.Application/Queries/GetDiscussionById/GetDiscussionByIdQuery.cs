using PetFamily.Core.Abstractions;

namespace PerFamily.Discussion.Application.Queries.GetDiscussionById;

public record GetDiscussionByIdQuery(Guid DiscussionId) : IQuery;
