namespace PetFamily.SharedKernel;

public class Permissions
{
    public static class User
    {
        public const string UpdateSocialLinks = "user.update.sociallinks";
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
        public const string Create = "volunteer.create";
    }

    public static class Admin
    {
        public const string Create = "species.create";
        public const string Update = "species.update";
        public const string Delete = "species.delete";
    }
}
