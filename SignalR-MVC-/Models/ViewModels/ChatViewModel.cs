namespace SignalRMVC.Models.ViewModels;

public class ChatViewModel
{
    public const int MaxPrivateChatsAllowed = 4;
    public IList<PrivateChat> PrivateChats { get; set; }
    public string? UserId { get; set; }
    public bool AllowAddPrivateChat => PrivateChats == null || PrivateChats.Count < MaxPrivateChatsAllowed;
    public ChatViewModel() => PrivateChats = [];
}
