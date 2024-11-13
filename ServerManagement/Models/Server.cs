namespace ServerManagement.Models;

public class Server
{
    public int ServerId { get; set; }
    public bool IsOnline { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public Server()
    {
        Random random = new();
        int randomOnOff = random.Next(0, 2);
        IsOnline = randomOnOff == 1;
    }
}
