namespace PetFamily.SharedKernel;

public class Permissions
{
    public static class User
    {
        public const string GetVolunteerRequest = "user.get.volunteer.request";
        public const string UpdateFullName = "user.update.fullname";
        public const string UpdateSocialLinks = "user.update.sociallinks";
        public const string GetVolunteerRequests = "user.get.volunteer.requests";
    }
    
    public static class Volunteer
    {
        public const string DeletePet = "volunteer.pet.delete";
        public const string CreatePet = "volunteer.pet.create";
        public const string UpdatePet = "volunteer.pet.update"; 
        public const string Update = "volunteer.update";
        public const string Delete = "volunteer.delete";
        public const string UpdateRequisites = "volunteer.update.requisites";
    }

    public static class Participant
    {
        public const string CreateApplication = "user.сreate.application";
        public const string Create = "volunteer.create";
    }

    public static class Admin
    {
        public const string Create = "species.create";
        public const string Update = "species.update";
        public const string Delete = "species.delete";
        public const string ReviewApplication = "admin.review.application";
        public const string ApproveApplication = "admin.approve.application";
        public const string SendToRevision = "admin.send.to.revision";
        public const string RejectApplication = "admin.reject.application";
        public const string BanUser = "admin.ban.user";
        public const string UnbanUser = "admin.unban.user";
        public const string ExtendBanUser = "admin.extend.ban.user";
        public const string GetAllBanUsers = "users.get.banned";
        public const string GetVolunteerRequests = "admin.get.volunteer.requests";
    }
}
