using api.Models;
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