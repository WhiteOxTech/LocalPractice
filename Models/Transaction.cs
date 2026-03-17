namespace ShopManagementSystem.Models;

public class Transaction
{
    public int Id { get; set; }
    
    public DateTime Timestamp { get; set; }
    
    public string ServiceType { get; set; } = string.Empty;
    
    public decimal AmountIn { get; set; }
    
    public decimal AmountOut { get; set; }
    
    public decimal ServiceCharge { get; set; }
    
    public string CreatedBy { get; set; } = string.Empty;
}
