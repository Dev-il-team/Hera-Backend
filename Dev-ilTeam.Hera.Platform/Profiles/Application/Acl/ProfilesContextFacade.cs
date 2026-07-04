using Dev_ilTeam.Hera.Platform.Profiles.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Profiles.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.Queries;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Model.ValueObjects;
using Dev_ilTeam.Hera.Platform.Profiles.Interfaces.Acl;

namespace Dev_ilTeam.Hera.Platform.Profiles.Application.Acl;

public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService
) : IProfilesContextFacade
{
    public async Task<int> CreateProfile(string firstName, string lastName, string email, string street, string number,
        string city,
        string postalCode, string country, CancellationToken cancellationToken)
    {
        var createProfileCommand =
            new CreateProfileCommand(firstName, lastName, email, street, number, city, postalCode, country);
        var result = await profileCommandService.Handle(createProfileCommand, cancellationToken);
        return result.Value?.Id ?? 0;
    }

    public async Task<int> FetchProfileIdByEmail(string email, CancellationToken cancellationToken)
    {
        var getProfileByEmailQuery = new GetProfileByEmailQuery(new EmailAddress(email));
        var profile = await profileQueryService.Handle(getProfileByEmailQuery, cancellationToken);
        return profile?.Id ?? 0;
    }
}
