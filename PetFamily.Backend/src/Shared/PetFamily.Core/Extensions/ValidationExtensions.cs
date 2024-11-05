using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using PetFamily.SharedKernel;

namespace PetFamily.Core.Extensions;

public static class ValidationExtensions
{
    public static ErrorList ToList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = Error.Deserialize(errorMessage)
            select Error.Validation(error.Code, error.Message, validationError.PropertyName);

        return errors.ToList();
    }

    public static ErrorList ToList(this IEnumerable<IdentityError> result) =>
       new (result.Select(r => Error.Validation(r.Code, r.Description)));
    
}