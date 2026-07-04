namespace Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.ValueObjects;

public record StreetAddress(string Street, string Number, string City, string PostalCode, string Country)
{
    public StreetAddress() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public StreetAddress(string street) : this(street, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public StreetAddress(string street, string number, string ciry) : this(street, number, ciry, string.Empty,
        string.Empty)
    {
    }

    public StreetAddress(string street, string number, string ciry, string postalCode) : this(street, number, ciry,
        postalCode, string.Empty)
    {
    }

    public string FullAddress => $"{Street} {Number}, {City}, {PostalCode}, {Country}";
}
