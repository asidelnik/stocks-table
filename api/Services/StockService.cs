using api.Models;

namespace api.Services;

public class StockService : BackgroundService
{
  private readonly Random random = new Random();
  public List<Stock> stocks = new List<Stock>
  {
    new Stock
    {
      Id = 1,
      StockName = "Stock1",
      BasePrice = 50.5,
      SupplyQty = 20,
      SupplyPrice = 70.3,
      DemandQty = 30,
      DemandPrice = 80.8,
      LastPrice = 90.9,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 2,
      StockName = "Stock2",
      BasePrice = 60.6,
      SupplyQty = 40,
      SupplyPrice = 80.8,
      DemandQty = 50,
      DemandPrice = 90.9,
      LastPrice = 100.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 3,
      StockName = "Stock3",
      BasePrice = 70.7,
      SupplyQty = 60,
      SupplyPrice = 90.9,
      DemandQty = 70,
      DemandPrice = 100.1,
      LastPrice = 110.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 4,
      StockName = "Stock4",
      BasePrice = 80.8,
      SupplyQty = 80,
      SupplyPrice = 100.1,
      DemandQty = 90,
      DemandPrice = 110.1,
      LastPrice = 120.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 5,
      StockName = "Stock5",
      BasePrice = 90.9,
      SupplyQty = 100,
      SupplyPrice = 110.1,
      DemandQty = 110,
      DemandPrice = 120.1,
      LastPrice = 130.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 6,
      StockName = "Stock6",
      BasePrice = 100.1,
      SupplyQty = 120,
      SupplyPrice = 130.1,
      DemandQty = 130,
      DemandPrice = 140.1,
      LastPrice = 150.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 7,
      StockName = "Stock7",
      BasePrice = 110.1,
      SupplyQty = 140,
      SupplyPrice = 150.1,
      DemandQty = 150,
      DemandPrice = 160.1,
      LastPrice = 170.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 8,
      StockName = "Stock8",
      BasePrice = 120.1,
      SupplyQty = 160,
      SupplyPrice = 170.1,
      DemandQty = 170,
      DemandPrice = 180.1,
      LastPrice = 190.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 9,
      StockName = "Stock9",
      BasePrice = 130.1,
      SupplyQty = 180,
      SupplyPrice = 190.1,
      DemandQty = 190,
      DemandPrice = 200.1,
      LastPrice = 210.1,
      UpdateTime = DateTime.Now
    },
    new Stock
    {
      Id = 10,
      StockName = "Stock10",
      BasePrice = 140.1,
      SupplyQty = 200,
      SupplyPrice = 210.1,
      DemandQty = 210,
      DemandPrice = 220.1,
      LastPrice = 230.1,
      UpdateTime = DateTime.Now
    }
  };

  public async Task<List<Stock>> GetStocks()
  {
    if (stocks.Count != 0)
    {
      UpdateStocks();
    }
    return await Task.FromResult(stocks);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      Console.WriteLine("StockService is running.");
      UpdateStocks();
      // Random delay between 1 and 5 seconds.
      int delay = random.Next(1, 6);
      await Task.Delay(TimeSpan.FromSeconds(delay), stoppingToken);
    }
  }

  public void UpdateStocks()
  {
    foreach (var stock in stocks)
    {
      stock.SupplyQty = random.Next(1, 100);
      stock.SupplyPrice = random.NextDouble() * 100;
      stock.DemandQty = random.Next(1, 100);
      stock.DemandPrice = random.NextDouble() * 100;
      stock.LastPrice = random.NextDouble() * 100;
      stock.UpdateTime = DateTime.Now;
    }
  }
}