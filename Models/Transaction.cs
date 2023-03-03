namespace ToolMart.Models;

public class Transaction : Model
{
    public DateTime Date { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public int TotalQuantity { get; set; }
    public decimal Price { get; set; }
}