using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Domain;

public class VolunteerRequest : SharedKernel.Entity<VolunteerRequestId>
{
    public Guid UserId { get; private set; }
    public Guid DiscussionId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Description RejectionDescription { get; private set; }
    public VolunteerInformation Information { get; private set; }
    public TypeRequest Status { get; private set; }

    private VolunteerRequest(VolunteerRequestId id) : base(id)
    {
    }

    private VolunteerRequest(
        VolunteerRequestId id,
        Guid userId,
        Guid discussionId,
        DateTime createdAt,
        VolunteerInformation information,
        TypeRequest status) : base(id)
    {
        UserId = userId;
        DiscussionId = discussionId;
        CreatedAt = createdAt;
        Information = information;
        Status = status;
    }

    public static VolunteerRequest CreateRequest(
        VolunteerRequestId id,
        Guid userId,
        Guid discussionId,
        VolunteerInformation information) =>
        new(id, userId, discussionId, DateTime.UtcNow, information, TypeRequest.Submitted);


    public Result Considered()
    {
        Status = TypeRequest.Considered;
        return Result.Success();
    }

    public Result Approve()
    {
        Status = TypeRequest.Approved;
        return Result.Success();
    }

    public Result Reject(Description reasonReject)
    {
        Status = TypeRequest.Rejected;
        RejectionDescription = reasonReject;
        return Result.Success();
    }

    public Result SendToRevision(Description description)
    {
        Status = TypeRequest.RevisionRequired;
        RejectionDescription = description;
        return Result.Success();
    }
}
