import React, { useEffect, useState } from 'react';
import { Typography, Box, Paper } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import axios from 'axios';
import { BASE_URL } from '../../config';
import { useParams } from 'react-router-dom';

// interface TradeTransactionDto {
//   id: number;
//   tradeDate: string;
//   settleDate: string;
//   unitCost: number;
//   quantity: number;
//   transactionAmount: number;
//   isSold: boolean;
// }

// interface DividendDto {
//   id: number;
//   amount: number;
//   tradeDate: string;
// }

// interface TradeFeeDto {
//   id: number;
//   amount: number;
//   tradeDate: string;
// }

interface StockProfitAndLossDataDto {
  stockId: number;
  stockName: string;
  quantityHeld: number;
  currentPrice: number;
  grossProfit: number;
  totalDividends: number;
  totalFees: number;
  // buyTransactions: TradeTransactionDto[];
  // sellTransactions: TradeTransactionDto[];
  // dividends: DividendDto[];
  // fees: TradeFeeDto[];
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

const BySnapshotSummaryPriceGroupId: React.FC = () => {  
  const { priceGroupId } = useParams<{ priceGroupId: string }>();
  const [data, setData] = useState<StockProfitAndLossDataDto[]>([]);

  useEffect(() => {
    axios.get<StockProfitAndLossDataDto[]>(`${BASE_URL}/StockAnalytics/get-by-snapshotStockPriceGroupId?snapshotStockPriceGroupId=${priceGroupId}`)
      .then(res => setData(res.data))
      .catch(err => console.error(err));
  }, []);

  return (
    <Box sx={{ p: 3 }}>
      <Typography variant="h4" gutterBottom>Stock Profit & Loss Summary</Typography>
      <Paper elevation={2} sx={{ height: 600 }}>
        <DataGrid
          rows={data}
          columns={columns}
          getRowId={(row) => row.stockId}
          sx={{ '& .MuiDataGrid-columnHeaderTitle': { fontWeight: 'bold' } }}
        />
      </Paper>
    </Box>
  );
};

export default BySnapshotSummaryPriceGroupId;
