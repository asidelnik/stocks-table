import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { StocksTableProps } from '../types/StocksTableProps';
import { StockClientType } from '../types/Stock';

export default function MUITable({ stocks }: StocksTableProps) {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Stock Id</TableCell>
            <TableCell>Stock Name</TableCell>
            <TableCell>Supply Quantity</TableCell>

            <TableCell>Supply Price</TableCell>
            <TableCell>Demand Quantity</TableCell>
            <TableCell>Demand Price</TableCell>

            <TableCell>Last Price</TableCell>
            <TableCell>Total Supply</TableCell>
            <TableCell>Total Demand</TableCell>

            <TableCell>Percentage Change</TableCell>
            <TableCell>Update Date</TableCell>
            <TableCell>Update Time</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {stocks.map((stock: StockClientType) => {
            const date = new Date(stock.updateTime);
            const formattedDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
            const formattedTime = `${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}:${date.getSeconds().toString().padStart(2, '0')}`;
            return (
              <TableRow
                key={stock.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">{stock.id}</TableCell>
                <TableCell component="th" scope="row">{stock.stockName}</TableCell>
                <TableCell>{stock.supplyQty}</TableCell>

                <TableCell>{stock.supplyPrice.toFixed(1)}</TableCell>
                <TableCell>{stock.demandQty}</TableCell>
                <TableCell>{stock.demandPrice.toFixed(1)}</TableCell>

                <TableCell>{stock.lastPrice.toFixed(1)}</TableCell>
                <TableCell>{stock.totalSupply.toFixed(1)}</TableCell>
                <TableCell>{stock.totalDemand.toFixed(1)}</TableCell>

                <TableCell style={{ color: stock.isPercentageChangePositive === null ? 'black' : stock.isPercentageChangePositive ? 'green' : 'red' }}>
                  {stock.percentageChange.toFixed(1)}%
                </TableCell>
                <TableCell>{formattedDate}</TableCell>
                <TableCell>{formattedTime}</TableCell>
              </TableRow>
            )
          })}
        </TableBody>
      </Table>
    </TableContainer>
  );
}