using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController : ControllerBase
{
  private readonly StockService _stockService;

  public StockController()
  {
    _stockService = new StockService();
  }

  /// <summary>
  /// Retrieves the list of stocks.
  /// GET /api/stocks
  /// GET /api/stocks?lastFetchTime=2022-01-01T00:00:00
  /// If lastFetchTime is provided, only stocks updated after this time will be returned.
  /// If lastFetchTime is not provided, all stocks will be returned.
  /// </summary>
  /// <returns>If no stocks found, returns 404 Not Found, if found, returns them, if error, returns 500</returns>  
  [HttpGet]
  public async Task<IActionResult> Get([FromQuery] DateTime? lastFetchTime)
  {
    IActionResult result;
    try
    {
      var stocks = await _stockService.GetStocks(lastFetchTime);
      if (stocks == null)
      {
        result = NotFound();
      }
      else
      {
        var stockList = stocks.ToArray();
        result = Ok(stockList);
      }
    }
    catch (Exception ex)
    {
      // Log the exception here
      result = StatusCode(500, "Internal server error");
    }
    return result;
  }
}