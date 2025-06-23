namespace UserOrderSystem.Models;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsPaid { get; set; } = false;
}
