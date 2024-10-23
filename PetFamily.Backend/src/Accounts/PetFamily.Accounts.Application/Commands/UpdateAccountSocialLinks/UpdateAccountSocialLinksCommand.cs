using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto;

namespace PetFamily.Accounts.Application.Commands.UpdateAccountSocialLinks;

public record UpdateAccountSocialLinksCommand(Guid UserId, IEnumerable<SocialLinkDto> SocialLinks) : ICommand;
