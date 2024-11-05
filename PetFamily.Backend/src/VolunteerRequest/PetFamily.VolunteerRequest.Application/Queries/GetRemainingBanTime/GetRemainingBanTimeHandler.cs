using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetRemainingBanTime;

public class GetRemainingBanTimeHandler(IVolunteerRequestReadDbContext context) :
    IQueryHandler<TimeSpan, GetRemainingBanTimeQuery>
{
    public async Task<Result<TimeSpan, ErrorList>> Execute(
        GetRemainingBanTimeQuery query, CancellationToken cancellationToken = default)
    {
        var user = await context.UserRestrictions.FirstOrDefaultAsync(b => b.UserId == query.ParticipantId,
            cancellationToken);

        if (user == null)
            return Errors.General.NotFound(query.ParticipantId).ToErrorList();

        return user.BannedUntil - DateTime.UtcNow;
    }
}
