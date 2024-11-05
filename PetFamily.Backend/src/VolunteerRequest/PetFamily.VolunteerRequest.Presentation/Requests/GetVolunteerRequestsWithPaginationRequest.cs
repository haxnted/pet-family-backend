namespace PetFamily.VolunteerRequest.Presentation.Requests;

public record GetVolunteerRequestsWithPaginationRequest(string? SortBy, string? SortDirection, int Page, int PageSize);