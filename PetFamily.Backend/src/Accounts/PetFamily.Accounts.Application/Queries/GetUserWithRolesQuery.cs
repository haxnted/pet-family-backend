using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.Queries;

public record GetUserWithRolesQuery(Guid UserId) : IQuery;
