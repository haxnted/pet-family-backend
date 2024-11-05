using FluentAssertions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;


namespace PetFamily.VolunteerRequest.Domain.UnitTests;

public class VolunteerRequestTests
{
    [Fact]
    public void CreateRequest_ShouldInitializeWithSubmittedStatus()
    {
        // Arrange
        var id = VolunteerRequestId.NewId();
        var userId = Guid.NewGuid();
        var discussionId = Guid.NewGuid();
        var information = new VolunteerInformation(FullName.Create("John", "Doe", null).Value,
            AgeExperience.Create(5).Value,
            PhoneNumber.Create("123-456-7890").Value,
            Description.Create("A volunteer with experience").Value,
            []);

        // Act
        var volunteerRequest = VolunteerRequest.CreateRequest(id, userId, information);

        // Assert
        volunteerRequest.Id.Should().Be(id);
        volunteerRequest.UserId.Should().Be(userId);
        volunteerRequest.Information.Should().Be(information);
        volunteerRequest.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        volunteerRequest.Status.Should().Be(TypeRequest.Submitted);
        volunteerRequest.RejectionDescription.Should().BeNull();
    }

    [Fact]
    public void Considered_ShouldChangeStatusToConsidered()
    {
        // Arrange
        var volunteerRequest = CreateDefaultVolunteerRequest();

        var adminId = Guid.NewGuid();
        // Act
        var result = volunteerRequest.Considered(adminId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteerRequest.Status.Should().Be(TypeRequest.Considered);
        volunteerRequest.RejectionDescription.Should().BeNull();
    }

    [Fact]
    public void Approve_ShouldChangeStatusToApproved()
    {
        // Arrange
        var volunteerRequest = CreateDefaultVolunteerRequest();

        // Act
        var result = volunteerRequest.Approve();

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteerRequest.Status.Should().Be(TypeRequest.Approved);
        volunteerRequest.RejectionDescription.Should().BeNull();
    }

    [Fact]
    public void Reject_ShouldChangeStatusToRejectedAndSetRejectionDescription()
    {
        // Arrange
        var volunteerRequest = CreateDefaultVolunteerRequest();
        var rejectionDescription = ValueObjects.RejectionDescription.Create("Not qualified enough").Value;

        // Act
        var result = volunteerRequest.Reject(rejectionDescription);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteerRequest.Status.Should().Be(TypeRequest.Rejected);
        volunteerRequest.RejectionDescription.Should().Be(rejectionDescription);
    }

    [Fact]
    public void SendToRevision_ShouldChangeStatusToRevisionRequiredAndSetRejectionDescription()
    {
        // Arrange
        var volunteerRequest = CreateDefaultVolunteerRequest();
        var revisionDescription = ValueObjects.RejectionDescription.Create("More information needed").Value;

        // Act
        var result = volunteerRequest.SendToRevision(revisionDescription);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteerRequest.Status.Should().Be(TypeRequest.RevisionRequired);
        volunteerRequest.RejectionDescription.Should().Be(revisionDescription);
    }

    private VolunteerRequest CreateDefaultVolunteerRequest()
    {
        var id = VolunteerRequestId.NewId();
        var userId = Guid.NewGuid();
        var information = new VolunteerInformation(FullName.Create("John", "Doe", null).Value,
            AgeExperience.Create(5).Value,
            PhoneNumber.Create("123-456-7890").Value,
            Description.Create("A volunteer with experience").Value,
            []);

        return VolunteerRequest.CreateRequest(id, userId, information);
    }
}
