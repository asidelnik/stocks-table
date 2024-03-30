using api.Models;
namespace api.Services;

public class StockService : BackgroundService
{
  private readonly Random random = new Random();

  /// <summary>
  /// Retrieves the list of stocks.
  /// </summary>
  /// <param name="lastFetchTime">If provided, only stocks updated after this time will be returned.</param>
  /// <returns>A list of stocks.</returns>  
  public async Task<List<Stock>> GetStocks(DateTime? lastFetchTime = null)
  {
    if (stocks.Count != 0)
    {
      UpdateStocks();
    }

    if (lastFetchTime.HasValue)
    {
      // Filter the stocks to return only those that were updated after lastFetchTime
      return await Task.FromResult(stocks.Where(stock => stock.UpdateTime > lastFetchTime.Value).ToList());
    }
    else
    {
      return stocks;
    }
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    // while (!stoppingToken.IsCancellationRequested)
    // {
    //   UpdateStocks();
    //   // Random delay between 1 and 5 seconds.
    //   int delay = random.Next(1, 6);
    //   await Task.Delay(TimeSpan.FromSeconds(delay), stoppingToken);
    // }
  }


  /// <summary>
  /// Updates random stocks: prices and quantities
  /// </summary>
  public void UpdateStocks()
  {
    List<int> stockIdsToUpdate = GetRandomStockIdsToUpdate();
    foreach (var stock in stocks)
    {
      if (stockIdsToUpdate.Contains(stock.Id))
      {
        stock.SupplyQty = random.Next(1, 100);
        stock.SupplyPrice = random.NextDouble() * 100;
        stock.DemandQty = random.Next(1, 100);
        stock.DemandPrice = random.NextDouble() * 100;
        stock.LastPrice = random.NextDouble() * 100;
        stock.UpdateTime = DateTime.UtcNow;
      }
    }
  }

  /// <summary>
  /// Generates a list of random stock IDs to update.
  /// </summary>
  /// <returns>A list of random stock IDs.</returns>
  private List<int> GetRandomStockIdsToUpdate()
  {
    int numberOfStocksToUpdate = random.Next(1, stocks.Count + 1);
    return stocks.OrderBy(s => random.Next())
      .Take(numberOfStocksToUpdate)
      .Select(s => s.Id)
      .ToList();
  }

  // Hardcoded list of stocks
  public List<Stock> stocks = new List<Stock>
  {
    new Stock
    {
      Id = 1001,
      StockName = "Stock1",
      BasePrice = 50.5,
      SupplyQty = 20,
      SupplyPrice = 70.3,
      DemandQty = 30,
      DemandPrice = 80.8,
      LastPrice = 90.9,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1002,
      StockName = "Stock2",
      BasePrice = 60.6,
      SupplyQty = 40,
      SupplyPrice = 80.8,
      DemandQty = 50,
      DemandPrice = 90.9,
      LastPrice = 100.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1003,
      StockName = "Stock3",
      BasePrice = 70.7,
      SupplyQty = 60,
      SupplyPrice = 90.9,
      DemandQty = 70,
      DemandPrice = 100.1,
      LastPrice = 110.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1004,
      StockName = "Stock4",
      BasePrice = 80.8,
      SupplyQty = 80,
      SupplyPrice = 100.1,
      DemandQty = 90,
      DemandPrice = 110.1,
      LastPrice = 120.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1005,
      StockName = "Stock5",
      BasePrice = 90.9,
      SupplyQty = 100,
      SupplyPrice = 110.1,
      DemandQty = 110,
      DemandPrice = 120.1,
      LastPrice = 130.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1006,
      StockName = "Stock6",
      BasePrice = 100.1,
      SupplyQty = 120,
      SupplyPrice = 130.1,
      DemandQty = 130,
      DemandPrice = 140.1,
      LastPrice = 150.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1007,
      StockName = "Stock7",
      BasePrice = 110.1,
      SupplyQty = 140,
      SupplyPrice = 150.1,
      DemandQty = 150,
      DemandPrice = 160.1,
      LastPrice = 170.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1008,
      StockName = "Stock8",
      BasePrice = 120.1,
      SupplyQty = 160,
      SupplyPrice = 170.1,
      DemandQty = 170,
      DemandPrice = 180.1,
      LastPrice = 190.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1009,
      StockName = "Stock9",
      BasePrice = 130.1,
      SupplyQty = 180,
      SupplyPrice = 190.1,
      DemandQty = 190,
      DemandPrice = 200.1,
      LastPrice = 210.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    },
    new Stock
    {
      Id = 1010,
      StockName = "Stock10",
      BasePrice = 140.1,
      SupplyQty = 200,
      SupplyPrice = 210.1,
      DemandQty = 210,
      DemandPrice = 220.1,
      LastPrice = 230.1,
      UpdateTime = DateTime.UtcNow.AddHours(-1)
    }
  };
}