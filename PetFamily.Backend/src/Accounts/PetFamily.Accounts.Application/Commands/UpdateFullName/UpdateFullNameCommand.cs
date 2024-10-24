using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto;

namespace PetFamily.Accounts.Application.Commands.UpdateFullName;

public record UpdateFullNameCommand(Guid UserId, FullNameDto FullNameDto) : ICommand;
