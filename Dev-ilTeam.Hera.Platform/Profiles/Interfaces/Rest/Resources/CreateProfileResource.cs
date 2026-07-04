namespace Dev_ilTeam.Hera.Platform.Profiles.Interfaces.Rest.Resources;

public record CreateProfileResource(
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country);
