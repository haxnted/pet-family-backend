using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.Discussions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Application.Queries.GetDiscussionById;

public class GetDiscussionByIdHandler(
    IValidator<GetDiscussionByIdQuery> validator,
    IDiscussionReadDbContext context)
    : IQueryHandler<DiscussionDto, GetDiscussionByIdQuery>
{
    public async Task<Result<DiscussionDto, ErrorList>> Execute(
        GetDiscussionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var discussion = await GetDiscussionById(query.DiscussionId, cancellationToken);
        if (discussion is null)
            return Errors.General.NotFound(query.DiscussionId).ToErrorList();

        return discussion;
    }

    private async Task<DiscussionDto?> GetDiscussionById(Guid id, CancellationToken cancellationToken) =>
       await context.Discussions
           .Include(d => d.Messages)
           .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
}