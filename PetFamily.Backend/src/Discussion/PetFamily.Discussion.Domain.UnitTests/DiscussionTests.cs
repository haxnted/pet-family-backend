using FluentAssertions;
using PerFamily.Discussion.Domain.Entities;
using PerFamily.Discussion.Domain.EntityIds;
using PerFamily.Discussion.Domain.Enums;
using PerFamily.Discussion.Domain.ValueObjects;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using Xunit;

namespace PetFamily.Discussion.Domain.UnitTests;

using Discussion = PerFamily.Discussion.Domain.Discussion;

public class DiscussionTests
{
    private readonly Users _users;
    private readonly Guid _validUserId;
    private readonly Guid _invalidUserId;
    private readonly DiscussionId _discussionId;
    private readonly MessageId _messageId;
    private readonly Message _message;

    public DiscussionTests()
    {
        _validUserId = Guid.NewGuid();
        _invalidUserId = Guid.NewGuid();
        _users = new Users(_validUserId, Guid.NewGuid());
        _discussionId = DiscussionId.NewId();
        _messageId = MessageId.NewId();
        _message = new Message(_messageId, _validUserId, Description.Create("Initial message").Value, false, DateTime.UtcNow);
    }

    [Fact]
    public void Create_ShouldInitializeDiscussionWithOpenStatus()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);
        
        discussion.Should().NotBeNull();
        discussion.Status.Should().Be(DiscussionStatus.Open);
        discussion.Messages.Should().BeEmpty();
    }

    [Fact]
    public void AddMessage_ShouldAddMessage_WhenUserExists()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);

        var result = discussion.AddMessage(_validUserId, _message);
        
        result.IsSuccess.Should().BeTrue();
        discussion.Messages.Should().ContainSingle()
            .Which.Should().Be(_message);
    }

    [Fact]
    public void AddMessage_ShouldReturnNotFoundError_WhenUserDoesNotExist()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);

        var result = discussion.AddMessage(_invalidUserId, _message);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.NotFound(_invalidUserId));
        discussion.Messages.Should().BeEmpty();
    }

    [Fact]
    public void DeleteMessage_ShouldRemoveMessage_WhenUserIsCreator()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);
        discussion.AddMessage(_validUserId, _message);

        var result = discussion.DeleteMessage(_validUserId, _messageId);

        result.IsSuccess.Should().BeTrue();
        discussion.Messages.Should().BeEmpty();
    }

    [Fact]
    public void DeleteMessage_ShouldReturnNotFoundError_WhenMessageDoesNotExist()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);

        var result = discussion.DeleteMessage(_validUserId, _messageId);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.NotFound(_messageId));
    }

    [Fact]
    public void DeleteMessage_ShouldReturnNonCreatorError_WhenUserIsNotCreator()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);
        var otherUserMessage = new Message(_messageId, Guid.NewGuid(), Description.Create("Message").Value, false, DateTime.UtcNow);
        discussion.AddMessage(_validUserId, otherUserMessage);

        var result = discussion.DeleteMessage(_validUserId, _messageId);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.Discussion.NonCreator(_validUserId));
        discussion.Messages.Should().ContainSingle().Which.Should().Be(otherUserMessage);
    }

    [Fact]
    public void EditingMessage_ShouldUpdateMessage_WhenUserIsCreator()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);
        discussion.AddMessage(_validUserId, _message);
        string newText = "Updated message";

        var result = discussion.EditingMessage(_validUserId, _messageId, newText);

        result.IsSuccess.Should().BeTrue();
        discussion.Messages.Should().ContainSingle()
            .Which.Text.Value.Should().Be(newText);
    }

    [Fact]
    public void EditingMessage_ShouldReturnNotFoundError_WhenMessageDoesNotExist()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);
        string newText = "Updated message";

        var result = discussion.EditingMessage(_validUserId, _messageId, newText);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.NotFound(_messageId));
    }

    [Fact]
    public void EditingMessage_ShouldReturnNonCreatorError_WhenUserIsNotCreator()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);
        var otherUserMessage = new Message(_messageId, Guid.NewGuid(), Description.Create("Message").Value, false, DateTime.UtcNow);
        discussion.AddMessage(_validUserId, otherUserMessage);
        string newText = "Updated message";

        var result = discussion.EditingMessage(_validUserId, _messageId, newText);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.Discussion.NonCreator(_validUserId));
        discussion.Messages.Should().ContainSingle()
            .Which.Text.Value.Should().Be("Message");
    }

    [Fact]
    public void Close_ShouldSetDiscussionStatusToClosed()
    {
        var discussion = Discussion.Create(_discussionId, Guid.NewGuid(), _users);

        discussion.Close();

        discussion.Status.Should().Be(DiscussionStatus.Closed);
    }
}