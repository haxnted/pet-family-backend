using PetFamily.Core.Dto.Discussions;

namespace PerFamily.Discussion.Application;

public interface IDiscussionReadDbContext
{
    public IQueryable<DiscussionDto> Discussions { get; }
    public IQueryable<MessageDto> Messages { get; }
}
