﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.EntityIds;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Application.Features.Volunteers;

public interface IVolunteersRepository
{
    public Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    public Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken = default);

    public Task<Result<Guid, Error>> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    public Task<Result<Volunteer, Error>> GetByPhoneNumber(PhoneNumber requestNumber,
        CancellationToken cancellationToken = default);

    public Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
}