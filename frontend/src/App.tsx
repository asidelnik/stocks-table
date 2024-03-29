import { useEffect, useState } from 'react'
import './App.css'
import { StockType } from './types/Stock';

function App() {
  const [stocks, setStocks] = useState<StockType[]>([]);

  useEffect(() => {
    getStockData();
  }, []);

  function getStockData() {
    fetch('http://localhost:5195/api/stock')
      .then(response => response.json())
      .then((data: StockType[]) => {
        console.log(data)
        setStocks(data)
      });
  }

  return (
    <>
      <div>
        <button onClick={getStockData}>Refresh data</button>
        {stocks.length > 0 ? (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>Stock Name</th>
                <th>Supply Quantity</th>
                <th>Supply Price</th>
                <th>Demand Quantity</th>
                <th>Demand Price</th>
                <th>Last Price</th>
                <th>Update Time</th>
              </tr>
            </thead>
            <tbody>
              {stocks.map((stock: StockType) => (
                <tr key={stock.id}>
                  <td>{stock.id}</td>
                  <td>{stock.stockName}</td>
                  <td>{stock.supplyQty}</td>
                  <td>{stock.supplyPrice}</td>
                  <td>{stock.demandQty}</td>
                  <td>{stock.demandPrice}</td>
                  <td>{stock.lastPrice}</td>
                  <td>{new Date(stock.updateTime).toLocaleTimeString()}</td>
                </tr>
              ))}
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