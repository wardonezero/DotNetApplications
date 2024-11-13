using Microsoft.AspNetCore.Hosting.Server;
using ServerManagement.Components.Pages;
using System.Xml.Linq;

namespace ServerManagement.Models;

public static class ServersRepository
{

    private static List<Server> _servers =
    [
        new(){ ServerId = 1, Name = "Server1", City = "Chicago" },
        new(){ ServerId = 2, Name = "Server2", City = "Chicago" },
        new(){ ServerId = 3, Name = "Server3", City = "Chicago" },
        new(){ ServerId = 4, Name = "Server4", City = "Chicago" },
        new(){ ServerId = 5, Name = "Server5", City = "Rockford" },
        new(){ ServerId = 6, Name = "Server6", City = "Rockford" },
        new(){ ServerId = 7, Name = "Server7", City = "Rockford" },
        new(){ ServerId = 8, Name = "Server8", City = "Rockford" },
        new(){ ServerId = 9, Name = "Server9", City = "Springfield" },
        new(){ ServerId = 10, Name = "Server10", City = "Springfield" },
        new(){ ServerId = 11, Name = "Server11", City = "Joliet" },
        new(){ ServerId = 12, Name = "Server12", City = "Joliet" },
        new(){ ServerId = 13, Name = "Server13", City = "Peoria" },
        new(){ ServerId = 14, Name = "Server14", City = "Peoria" },
        new(){ ServerId = 14, Name = "Server15", City = "Peoria" },
        new(){ ServerId = 16, Name = "Server16", City = "Peoria" }
    ];

    public static Server? GetServerById(int id) => _servers.Where(s => s.ServerId == id).FirstOrDefault();

    public static void AddServer(Server server)
    {
        var maxId = _servers.Max(x => x.ServerId);
        server.ServerId = maxId + 1;
        _servers.Add(server);
    }

    public static List<Server> GetServres() => _servers;

    public static List<Server> GetServersByCity(string cityName)
        => _servers.Where(s => s.City.Equals(cityName, StringComparison.OrdinalIgnoreCase)).ToList();

    public static void UpdateServer(int serverId, Server server)
    {
        if (serverId != server.ServerId) return;
        var serverToUpdate = _servers.FirstOrDefault(s => s.ServerId == serverId);
        if (serverToUpdate != null)
        {
            serverToUpdate.IsOnline = server.IsOnline;
            serverToUpdate.Name = server.Name;
            serverToUpdate.City = server.City;
        }
    }

    public static void DeleteServer(int serverId)
    {
        var server = _servers.FirstOrDefault(s => s.ServerId == serverId);
        if (server != null)
            _servers.Remove(server);
    }

    public static List<Server> SearchServers(string serverFilter)
        => _servers.Where(s => s.Name.Contains(serverFilter, StringComparison.OrdinalIgnoreCase)).ToList();
}
