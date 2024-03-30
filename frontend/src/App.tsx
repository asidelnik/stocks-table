import { useEffect, useState } from 'react'
import './App.css'
import { StockClientType, StockServerType } from './types/Stock';

function App() {
  const [fetchTime, setFetchTime] = useState<Date | null>(null);
  const [stocks, setStocks] = useState<StockClientType[]>([]);

  useEffect(() => {
    getStockData();
  }, []);

  const getStockData = async () => {
    const response = await fetch(`http://localhost:5195/api/stock${fetchTime ? `?lastFetchTime=${fetchTime.toISOString()}` : ``}`);
    const updatedStocks: StockServerType[] = await response.json();
    console.log(updatedStocks);

    const updatedStocksWithSums: StockClientType[] = updatedStocks.map((stock: StockServerType) => {
      const totalSupply = stock.supplyQty * stock.supplyPrice; // This calculation was not explained in the spec. Pershaps should show in a last row that sums all stocks supplyQty 
      const totalDemand = stock.demandQty * stock.demandPrice;
      const percentageChange = ((stock.lastPrice / stock.basePrice) - 1) * 100

      return {
        ...stock,
        totalSupply: totalSupply,
        totalDemand: totalDemand,
        percentageChange: percentageChange,
        // isPercentageChangePositive: percentageChange > 0
      };
    });

    setStocks(prevStocks => {
      const updatedStocksMap = new Map(updatedStocksWithSums.map(stock => [stock.id, stock]));
      const newStocks = prevStocks.map(stock => updatedStocksMap.get(stock.id) || stock);

      const newStocksToAdd = Array.from(updatedStocksMap.values()).filter(stock => !newStocks.find(s => s.id === stock.id));
      return [...newStocks, ...newStocksToAdd].sort((a, b) => a.id - b.id);
    });

    setFetchTime(new Date(Date.now()));
  }

  return (
    <>
      <div>
        <button type='button' onClick={getStockData}>רענון נתונים</button>
        {stocks.length > 0 ? (
          <table>
            <thead>
              <tr>
                <th>Stock Id</th>
                <th>Stock Name</th>
                <th>Supply Quantity</th>
                <th>Supply Price</th>
                <th>Demand Quantity</th>
                <th>Demand Price</th>
                <th>Last Price</th>
                <th>Total Supply</th>
                <th>Total Demand</th>
                <th>Percentage Change</th>
                <th>Update Date</th>
                <th>Update Time</th>
              </tr>
            </thead>
            <tbody>
              {stocks.map((stock: StockClientType) => {
                const date = new Date(stock.updateTime);
                const formattedDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
                const formattedTime = `${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}:${date.getSeconds().toString().padStart(2, '0')}`;
                return (
                  <tr key={stock.id}>
                    <td>{stock.id}</td>
                    <td>{stock.stockName}</td>
                    <td>{stock.supplyQty}</td>
                    <td>{stock.supplyPrice.toFixed(1)}</td>
                    <td>{stock.demandQty}</td>
                    <td>{stock.demandPrice.toFixed(1)}</td>
                    <td>{stock.lastPrice.toFixed(1)}</td>
                    <td>{stock.totalSupply.toFixed(1)}</td>
                    <td>{stock.totalDemand.toFixed(1)}</td>
                    <td>{stock.percentageChange.toFixed(1)}%</td>
                    <td>{formattedDate}</td>
                    <td>{formattedTime}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <p>No stocks available</p>
        )}
      </div>
    </>
  );
}

export default App