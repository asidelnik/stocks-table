import { useEffect, useState } from 'react'
import './App.css'
import { StockType } from './types/Stock';

function App() {
  const [stocks, setStocks] = useState<StockType[]>([]);

  useEffect(() => {
    getStockData();
  }, []);

  function getStockData() {
    fetch('https://api.example.com/stocks')
      .then(response => response.json())
      .then((data: StockType[]) => {
        console.log(data)
        setStocks(data)
      });
  }

  return (
    <>
      <div>
        {stocks.length > 0 ? (
          <ul>
            {stocks.map((stock: StockType) => (
              <li key={stock.StockId}>
                {stock.StockId} - {stock.StockName}
              </li>
            ))}
          </ul>
        ) : (
          <p>No stocks available</p>
        )}
      </div>
    </>
  )
}

export default App