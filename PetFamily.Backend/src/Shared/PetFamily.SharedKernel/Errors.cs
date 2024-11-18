using System.Runtime.InteropServices.JavaScript;

namespace PetFamily.SharedKernel;

public static class Errors
{
    public static class General
    {
        public static Error NotOwner(Guid? id = null)
        {
            var Id = id == null ? "Id" : $"{id}";
            return Error.Forbidden("not.owner", $"You are not the owner of {Id}");
        }
        public static Error AccessDenied(string? resource = null)
        {
            var label = resource ?? "resource";
            return Error.Validation($"{label}.access.denied", $"Access to {label} is denied");
        }
        public static Error AlreadyExist(string? name = null)
        {
            var label = name ?? "entity";
            return Error.Validation($"{label}.already.exist", $"{label} already exist");
        }

        public static Error AlreadyUsed(Guid? id = null)
        {
            var Id = id == null ? "Id" : $"{id}";
            return Error.Conflict("value.already.used", $"{Id} is already used. Operation impossible");
        }

        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.NotFound("record.not.found", $"record not found {forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid {label} length");
        }

        public static Error InsufficientItems(string? name = null)
        {
            var label = name ?? "items";
            return Error.Validation("insufficient.items", $"Insufficient number of {label} to complete the operation");
        }
    }

    public static class Token
    {
        public static Error ExpiredToken()
        {
            return Error.Validation("token.is.expired", "Your token is expired. Please, login again");
        }

        public static Error InvalidToken()
        {
            return Error.Validation("token.is.invalid", "Your token is invalid. Please, login again");
        }
    }
    public static class User
    {
        public static Error InvalidCredentials()
        {
            return Error.Validation("invalid.user.credentials", "Invalid user credentials");
        }
    }

    public static class UserRestriction
    {
        public static Error AlreadyBanned(Guid userId)
        {
            return Error.Conflict("user.already.banned", $"User with ID '{userId}' is already banned.");
        }
        
        public static Error NotBanned(Guid userId)
        {
            return Error.Conflict("user.not.banned", $"User with ID '{userId}' is not currently banned.");
        }
        
        public static Error BanExpired(Guid userId)
        {
            return Error.Conflict("user.ban.expired", $"The ban for user with ID '{userId}' has already expired.");
        }
        
        public static Error AccessDeniedForBannedUser(Guid userId)
        {
            return Error.Authorization("user.banned.access.denied", $"Access denied for banned user with ID '{userId}'.");
        }
        
        public static Error InvalidBanDuration()
        {
            return Error.Validation("ban.invalid.duration", "The specified ban duration is invalid.");
        }
    }
    public static class VolunteerRequest
    {
        public static Error RevisionRequired()
        {
            return Error.Conflict("volunteer.request.update.failed", "To update an application, it needs to have a status of 'Corrections Needed'");
        }
        public static Error AlreadyRejected()
        {
            return Error.Conflict("volunteer.request.already.rejected", "Volunteer request already rejected");
        }
        public static Error NonConsidered()
        {
            return Error.Conflict("volunteer.request.not.considered", "Volunteer request not considered");
        }
    }
    public static class Discussion
    {
        public static Error NonCreator(Guid id)
        {
            return Error.Conflict("invalid.owner.message", "You don't have rights to this message");
        }
    }
    public static class Model
    {
        public static Error AlreadyExist(string? name = null)
        {
            var label = name ?? "entity";
            return Error.Validation($"{label}.already.exist", $"{label} already exist");
        }
    }
}
