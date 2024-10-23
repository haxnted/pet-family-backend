using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto;

namespace PetFamily.Accounts.Application.Commands.UpdateAccountRequisites;

public record UpdateAccountRequisitesCommand(Guid UserId, IEnumerable<RequisiteDto> Requisites) : ICommand;
