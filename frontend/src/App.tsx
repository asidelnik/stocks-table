import { useEffect, useState } from 'react'
import './App.css'
import { StockClientType, StockServerType } from './types/Stock';
import MUITable from './components/MUITable';
import { Button } from '@mui/material';

function App() {
  const [fetchTime, setFetchTime] = useState<Date | null>(null);
  const [stocks, setStocks] = useState<StockClientType[]>([]);

  useEffect(() => {
    getStockData();
  }, []);

  // Fetches stock data from the server
  const getStockData = async () => {
    const response = await fetch(`http://localhost:5195/api/stocks${fetchTime ? `?lastFetchTime=${fetchTime.toISOString()}` : ``}`);
    const updatedStocks: StockServerType[] = await response.json();

    // Adds calculated properties to each stock
    const updatedStocksWithSums: StockClientType[] = updatedStocks.map((stock: StockServerType) => {
      const totalSupply = stock.supplyQty * stock.supplyPrice; // This calculation was not explained in the spec. Pershaps should show in a last row that sums all stock rows' supplyQty.
      const totalDemand = stock.demandQty * stock.demandPrice;
      const percentageChange = ((stock.lastPrice / stock.basePrice) - 1) * 100

      return {
        ...stock,
        totalSupply: totalSupply,
        totalDemand: totalDemand,
        percentageChange: percentageChange,
      };
    });

    // Updates the stocks state with the new stock data & sorts by stock id
    setStocks(prevStocks => {
      const updatedStocksMap = new Map(updatedStocksWithSums.map(stock => [stock.id, stock]));

      const updatedPreviousStocks = prevStocks.map(stock => {
        const updatedStock: StockClientType | undefined = updatedStocksMap.get(stock.id);
        if (updatedStock) {
          return { ...updatedStock, isPercentageChangePositive: updatedStock.percentageChange > stock.percentageChange };
        }
        return stock;
      })

      // Code currently not needed because server always returns same (hard coded) stocks (same ids)
      const newStocks = Array.from(updatedStocksMap.values()).filter(stock => !updatedPreviousStocks.find(s => s.id === stock.id));
      return [...updatedPreviousStocks, ...newStocks].sort((a, b) => a.id - b.id);
    });

    setFetchTime(new Date(Date.now()));
  }

  return (
    <>
      <div className='refresh-button-container'>
        <Button variant="contained" type='button' onClick={getStockData} className='refresh-stocks-button'>רענון נתונים</Button>

        {stocks.length > 0 ? (
          <MUITable stocks={stocks} />
        ) : (
          <p>No stocks available</p>
        )}
      </div>
    </>
  );
}

export default App