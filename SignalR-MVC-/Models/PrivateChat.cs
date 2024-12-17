using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRMVC.Models;

public class PrivateChat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ChatId { get; set; }
    public required string ChatName { get; set; }
}
