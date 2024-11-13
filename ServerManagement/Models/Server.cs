using System.ComponentModel.DataAnnotations;

namespace ServerManagement.Models;

public class Server
{
    public int ServerId { get; set; }

    public bool IsOnline { get; set; }

    [Required]
    public required string Name { get; set; }//the required keyword is not working

    [Required]
    public required string City { get; set; }

    public Server()
    {
        Random random = new();
        int randomOnOff = random.Next(0, 2);
        IsOnline = randomOnOff == 1;
    }
}
