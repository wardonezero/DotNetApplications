namespace SignalRMVC.Models.ViewModels;

public class ChatViewModel
{
    public int MaxPrivateChatsAllowed { get; set; } = 4;
    public IList<PrivateChat> PrivateChats { get; set; }
    public string? UserId { get; set; }
    public bool AllowAddPrivateChat => PrivateChats == null || PrivateChats.Count < MaxPrivateChatsAllowed;
    public ChatViewModel() => PrivateChats = [];
}
