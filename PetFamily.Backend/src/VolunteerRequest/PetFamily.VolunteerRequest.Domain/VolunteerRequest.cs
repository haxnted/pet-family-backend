using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;


namespace PetFamily.VolunteerRequest.Domain;

public class VolunteerRequest : SharedKernel.Entity<VolunteerRequestId>
{
    public Guid UserId { get; private set; }
    public Guid InspectorId { get; set; }
    public Guid DiscussionId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public RejectionDescription? RejectionDescription { get; private set; }
    public VolunteerInformation Information { get; private set; }
    public TypeRequest Status { get; private set; }

    private VolunteerRequest(VolunteerRequestId id) : base(id)
    {
    }

    private VolunteerRequest(
        VolunteerRequestId id,
        Guid userId,
        DateTime createdAt,
        VolunteerInformation information,
        TypeRequest status) : base(id)
    {
        UserId = userId;
        CreatedAt = createdAt;
        Information = information;
        Status = status;
    }

    public static VolunteerRequest CreateRequest(
        VolunteerRequestId id,
        Guid userId,
        VolunteerInformation information) =>
        new(id, userId, DateTime.UtcNow, information, TypeRequest.Submitted);


    public UnitResult<Error> UpdateVolunteerInformation(VolunteerInformation volunteerInformation)
    {
        if (Status != TypeRequest.RevisionRequired)
            return Errors.VolunteerRequest.RevisionRequired();
        
        Information = volunteerInformation;
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> CreateDiscussion(Guid discussionId)
    {
        if (Status != TypeRequest.Considered)
            return Errors.VolunteerRequest.NonConsidered();

        DiscussionId = discussionId;
        return UnitResult.Success<Error>();
    }

    public Result Considered(Guid inspectorId)
    {
        InspectorId = inspectorId;
        Status = TypeRequest.Considered;
        return Result.Success();
    }

    public Result Approve()
    {
        Status = TypeRequest.Approved;
        return Result.Success();
    }

    public UnitResult<Error> Reject(RejectionDescription reasonReject)
    {
        if (Status == TypeRequest.Rejected)
            return Errors.VolunteerRequest.AlreadyRejected();
        Status = TypeRequest.Rejected;
        RejectionDescription = reasonReject;

        return UnitResult.Success<Error>();
    }

    public Result SendToRevision(RejectionDescription description)
    {
        Status = TypeRequest.RevisionRequired;
        RejectionDescription = description;
        return Result.Success();
    }
}
