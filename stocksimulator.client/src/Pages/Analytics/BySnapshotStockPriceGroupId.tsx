import React, { useEffect, useState } from 'react';
import {
  Typography,
  Box,
  Paper,
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { MdExpandMore } from 'react-icons/md';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import axios from 'axios';
import { BASE_URL } from '../../config';
import { useParams } from 'react-router-dom';

interface TradeTransactionDto {
    id: number;
    tradeDate: string;
    settleDate: string;
    unitCost: number;
    quantity: number;
    transactionAmount: number;
    isSold: boolean;
  }
  
  interface DividendDto {
    id: number;
    amount: number;
    tradeDate: string;
  }
  
  interface TradeFeeDto {
    id: number;
    amount: number;
    tradeDate: string;
  }
  
  interface StockProfitAndLossDataDto {
    stockId: number;
    stockName: string;
    quantityHeld: number;
    currentPrice: number;
    grossProfit: number;
    totalDividends: number;
    totalFees: number;
    buyTransactions: TradeTransactionDto[];
    sellTransactions: TradeTransactionDto[];
    dividends: DividendDto[];
    fees: TradeFeeDto[];
  }
  
  const columns: GridColDef[] = [
    { field: 'stockId', headerName: 'Stock ID', width: 100 },
    { field: 'stockName', headerName: 'Stock Name', width: 220 },
    { field: 'quantityHeld', headerName: 'Qty Held', width: 120 },
    { field: 'currentPrice', headerName: 'Current Price', width: 140 },
    { field: 'grossProfit', headerName: 'Gross Profit', width: 140 },
    { field: 'totalDividends', headerName: 'Dividends', width: 120 },
    { field: 'totalFees', headerName: 'Fees', width: 100 },
  ];

const BySnapshotStockPriceGroupId: React.FC = () => 
{
    const { priceGroupId } = useParams<{ priceGroupId: string }>();
    const [data, setData] = useState<StockProfitAndLossDataDto[]>([]);

    useEffect(() => {
        axios.get<StockProfitAndLossDataDto[]>(`${BASE_URL}/StockAnalytics/get-by-snapshotStockPriceGroupId?snapshotStockPriceGroupId=${priceGroupId}`)
            .then(res => setData(res.data))
            .catch(err => console.error(err));
    }, []);

    const renderTransactions = (transactions: TradeTransactionDto[], type: string) => (
        <Accordion>
          <AccordionSummary expandIcon={<MdExpandMore />} >
            <Typography>{type} Transactions</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <TableContainer>
              <Table size="small">
                <TableHead>
                  <TableRow>
                    <TableCell>ID</TableCell>
                    <TableCell>Trade Date</TableCell>
                    <TableCell>Settle Date</TableCell>
                    <TableCell>Unit Cost</TableCell>
                    <TableCell>Quantity</TableCell>
                    <TableCell>Amount</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {transactions.map(tx => (
                    <TableRow key={tx.id}>
                      <TableCell>{tx.id}</TableCell>
                      <TableCell>{tx.tradeDate}</TableCell>
                      <TableCell>{tx.settleDate}</TableCell>
                      <TableCell>{tx.unitCost}</TableCell>
                      <TableCell>{tx.quantity}</TableCell>
                      <TableCell>{tx.transactionAmount}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </AccordionDetails>
        </Accordion>
      );
    
      const renderExtras = (items: (DividendDto | TradeFeeDto)[], title: string) => (
        <Accordion>
          <AccordionSummary expandIcon={<MdExpandMore />} >
            <Typography>{title}</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <TableContainer>
              <Table size="small">
                <TableHead>
                  <TableRow>
                    <TableCell>ID</TableCell>
                    <TableCell>Amount</TableCell>
                    <TableCell>Trade Date</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {items.map((item) => (
                    <TableRow key={item.id}>
                      <TableCell>{item.id}</TableCell>
                      <TableCell>{item.amount}</TableCell>
                      <TableCell>{item.tradeDate}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </AccordionDetails>
        </Accordion>
      );
    
      return (
        <Box sx={{ p: 3 }}>
          <Typography variant="h4" gutterBottom>Stock Profit & Loss Summary</Typography>
          {data.map((stock) => (
            <Box key={stock.stockId} sx={{ mb: 3 }}>
              <Paper elevation={2} sx={{ p: 2 }}>
                <Typography variant="h6">{stock.stockName}</Typography>
                <Typography>Quantity Held: {stock.quantityHeld}</Typography>
                <Typography>Current Price: {stock.currentPrice}</Typography>
                <Typography>Gross Profit: {stock.grossProfit}</Typography>
                <Typography>Total Dividends: {stock.totalDividends}</Typography>
                <Typography>Total Fees: {stock.totalFees}</Typography>
    
                {renderTransactions(stock.buyTransactions, 'Buy')}
                {renderTransactions(stock.sellTransactions, 'Sell')}
                {renderExtras(stock.dividends, 'Dividends')}
                {renderExtras(stock.fees, 'Fees')}
              </Paper>
            </Box>
          ))}
        </Box>
      );
};

export default BySnapshotStockPriceGroupId;
