namespace api.Models;

public class Stock
{
  public int Id { get; set; }
  public required string StockName { get; set; }
  public double BasePrice { get; set; }
  public int SupplyQty { get; set; }
  public double SupplyPrice { get; set; }
  public int DemandQty { get; set; }
  public double DemandPrice { get; set; }
  public double LastPrice { get; set; }
  public DateTime UpdateTime { get; set; }
}