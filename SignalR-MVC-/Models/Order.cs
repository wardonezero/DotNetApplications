namespace SignalRMVC.Models;

public class Order
{
    public int OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int Count { get; set; }
}
