using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;

namespace Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;

public class Room
{
    public Room()
    {
        Name = string.Empty;
    }

    public Room(string name)
    {
        Name = name;
    }

    public Room(CreateRoomCommand command)
    {
        Name = command.Name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
