using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestById;

public class GetVolunteerRequestByIdHandler(IVolunteerRequestReadDbContext context)
    : IQueryHandler<VolunteerRequestDto, GetVolunteerRequestByIdCommand>
{
    public async Task<Result<VolunteerRequestDto, ErrorList>> Execute(
        GetVolunteerRequestByIdCommand query, CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await context.VolunteerRequests
            .FirstOrDefaultAsync(v => v.Id == query.VolunteerRequestId, cancellationToken);
        
        if (volunteerRequest is null)
            return Errors.General.NotFound(query.VolunteerRequestId).ToErrorList();

        if (volunteerRequest.UserId != query.PatricipantId)
            return Errors.General.NotOwner(query.PatricipantId).ToErrorList();
        
        return volunteerRequest;
    }
}
