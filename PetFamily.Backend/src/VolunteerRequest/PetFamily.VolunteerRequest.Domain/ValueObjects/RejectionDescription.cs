using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Domain.ValueObjects;

public record RejectionDescription
{
    public string Value { get; }

    private RejectionDescription(){}
    private RejectionDescription(string value) => Value = value;

    public static Result<RejectionDescription, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.EXTRA_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        return new RejectionDescription(description);
    }
}
