import React, { useEffect, useState } from 'react';
import { Button, Typography } from '@mui/material';
import Grid from '@mui/material/Grid';
import { DataGrid, GridColDef, GridRowSelectionModel } from '@mui/x-data-grid';
import axios from 'axios';
import { BASE_URL } from '../../config';
import { useParams } from 'react-router-dom';


const tradeTransactionColumns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 90 },
  {
    field: 'tradeDate', headerName: 'Trade Date', width: 150,
    align: 'center',
    headerAlign: 'center',
    renderCell: (params) =>
      <>
        {new Date(params.value).toLocaleDateString('en-GB')}
      </>,
  },
  {
    field: 'unitCost', headerName: 'Unit Cost', width: 130,
    align: 'right',
    headerAlign: 'right',
    renderCell: (params) => {
      return new Intl.NumberFormat('en-GB', {
        style: 'currency',
        currency: 'GBP',
        minimumFractionDigits: 2,
      }).format(params.value);
    }
  },
  {
    field: 'quantity', headerName: 'Quantity', width: 100,
    align: 'right',
    headerAlign: 'right',
  },
  {
    field: 'transactionAmount', headerName: 'Trx Amount', width: 150,
    align: 'right',
    headerAlign: 'right',
    renderCell: (params) => {
      return new Intl.NumberFormat('en-GB', {
        style: 'currency',
        currency: 'GBP',
        minimumFractionDigits: 2,
      }).format(params.value);
    }
  },
  {
    field: 'conversionRate', headerName: 'Conversion Rate', width: 150,
    align: 'right',
    headerAlign: 'right'
  },
  {
    field: 'unitCostForeign', headerName: 'Unit Cost Foreign', width: 150,
    align: 'right',
    headerAlign: 'right',
    renderCell: (params) => {
      return new Intl.NumberFormat('en-GB', {
        style: 'currency',
        currency: 'GBP',
        minimumFractionDigits: 2,
      }).format(params.value);
    }
  },
  { field: 'isSold', headerName: 'Is Sold', width: 150, type: 'boolean' },
];

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 90 },
  {
    field: 'tradeDate', headerName: 'Trade Date', width: 150,
    align: 'center',
    headerAlign: 'center',
    renderCell: (params) =>
      <>
        {new Date(params.value).toLocaleDateString('en-GB')}
      </>,
  },
  {
    field: 'amount', headerName: 'Amount', width: 150,
    align: 'right',
    headerAlign: 'right',
    renderCell: (params) => {
      return new Intl.NumberFormat('en-GB', {
        style: 'currency',
        currency: 'GBP',
        minimumFractionDigits: 2,
      }).format(params.value);
    }
  },
];

const ReviewMatches: React.FC = () => {
  const { buyerId, stockId } = useParams<{ buyerId: string; stockId: string }>();

  const [data1, setData1] = useState<any[]>([]);
  const [data2, setData2] = useState<any[]>([]);
  const [data3, setData3] = useState<any[]>([]);

  const [selection1, setSelection1] = useState<GridRowSelectionModel>([]);
  const [selection2, setSelection2] = useState<GridRowSelectionModel>([]);
  const [selection3, setSelection3] = useState<GridRowSelectionModel>([]);

  const [quantitySum, setQuantitySum] = useState<number>(0);
  const [transactionAmountSum, setTransactionAmountSum] = useState<number>(0);
  const [dividendAmountSum, setDividendAmountSum] = useState<number>(0);
  const [feeAmountSum, setFeeAmountSum] = useState<number>(0);
  const net = transactionAmountSum - feeAmountSum + dividendAmountSum;

  const safeSum = (values: number[]) =>
    values.reduce((acc, val) => Math.round((acc + val) * 100) / 100, 0);

  useEffect(() => {
    axios.get<any[]>(`${BASE_URL}/TradeTransaction/get-by-stockId-with-profitAndLossId?stockId=${stockId}&buyerId=1`).then(res => setData1(res.data));
    axios.get<any[]>(`${BASE_URL}/Dividend/get-by-stockId-with-profitAndLossId?stockId=${stockId}&buyerId=1`).then(res => setData2(res.data));
    axios.get<any[]>(`${BASE_URL}/TradeFee/get-by-stockId-with-profitAndLossId?stockId=${stockId}&buyerId=1`).then(res => setData3(res.data));
  }, []);

  useEffect(() => {
    const selectedRows = data1.filter(row => selection1.includes(row.id));
    const quantitySum = safeSum(
      selectedRows.map(row => row.isSold ? -row.quantity : row.quantity)
    );
    const trxSum = selectedRows.reduce((sum, row) =>
      sum + (row.isSold ? row.transactionAmount : -row.transactionAmount), 0);

    setQuantitySum(quantitySum);
    setTransactionAmountSum(trxSum);
  }, [selection1, data1]);

  useEffect(() => {
    const selectedRows = data2.filter(row => selection2.includes(row.id));
    const trxSum = safeSum(
      selectedRows.map(row => row.amount)
    );

    setDividendAmountSum(trxSum);
  }, [selection2, data2]);

  useEffect(() => {
    const selectedRows = data3.filter(row => selection3.includes(row.id));
    const trxSum = safeSum(
      selectedRows.map(row => row.amount)
    );

    setFeeAmountSum(trxSum);
  }, [selection3, data3]);


  const handleGroup = async () => {
    const payload = {
      tradeTransactionIds: selection1,
      dividendIds: selection2,
      tradeFeeIds: selection3,
    };

    try {
      console.log('Payload:', payload);
      await axios.post(`${BASE_URL}/ProfitAndLoss/group`, payload);
      alert("Group successful!");
      window.location.reload();
    } catch (error) {
      alert("Group failed.");
    }
    // } catch (error: any) { //Debug error handling (replace above))
    //   console.error(error);
    //   alert(`Group failed: ${error?.response?.data || error.message}`);
    // }
  };

  const tradeTransactionRenderGrid = (title: string, data: any[], selection: GridRowSelectionModel, setSelection1: (val: GridRowSelectionModel) => void) => (
    <div style={{ display: 'flex', flexDirection: 'column' }}>
      <Typography variant="h6">
        {title} {data.length > 0 ? `: ${data[0].stockName}` : ''}
      </Typography>
      <DataGrid
        sx={{
          '& .MuiDataGrid-columnHeaderTitle': {
            fontWeight: 'bold',
          }
        }}
        rows={data}
        columns={tradeTransactionColumns}
        getRowId={(row) => row.id}
        checkboxSelection
        rowSelectionModel={selection}
        onRowSelectionModelChange={(newSelection) => {
          setSelection1(newSelection);
        }}
      />
    </div>
  );

  const renderGrid = (title: string, data: any[], selection: GridRowSelectionModel, setSelection: (val: GridRowSelectionModel) => void) => (
    <div style={{ display: 'flex', flexDirection: 'column' }}>
      <Typography variant="h6">{title}</Typography>
      <DataGrid
        sx={{
          '& .MuiDataGrid-columnHeaderTitle': {
            fontWeight: 'bold',
          }
        }}
        rows={data}
        columns={columns}
        getRowId={(row) => row.id}
        checkboxSelection
        rowSelectionModel={selection}
        onRowSelectionModelChange={(newSelection) => setSelection(newSelection)}
      />
    </div>
  );

  const renderSummary = () => (

    <div style={{ marginBottom: '1rem' }}>
      <Typography variant="subtitle1">
        <strong>Summary Display</strong>
        &nbsp;
        Quantity Sum: {quantitySum.toFixed(1)}
      </Typography>
      <Typography variant="body1" color={transactionAmountSum >= 0 ? 'green' : 'red'}>
        Transaction Amount Sum: {transactionAmountSum.toLocaleString('en-GB', {
          style: 'currency',
          currency: 'GBP'
        })}
        ;

        Dividends Sum: {dividendAmountSum.toLocaleString('en-GB', {
          style: 'currency',
          currency: 'GBP'
        })}
        ;

        Fees Sum: {feeAmountSum.toLocaleString('en-GB', {
          style: 'currency',
          currency: 'GBP'
        })}
        ;&nbsp;

        <b>
          Net: {net.toLocaleString('en-GB', {
            style: 'currency',
            currency: 'GBP',
          })}
        </b>
      </Typography>
      <Button
        variant="contained"
        color="primary"
        style={{ marginTop: 8 }}
        disabled={quantitySum.toFixed(1) !== '0.0' || selection1.length === 0}
        onClick={handleGroup}
      >
        Group
      </Button>
    </div>);

  return (
    <>
      <Typography variant="h4" gutterBottom>About This App</Typography>

      {renderSummary()}

      <Grid container spacing={3}>
        {tradeTransactionRenderGrid('Trades', data1, selection1, setSelection1)}
        {renderGrid('Dividends', data2, selection2, setSelection2)}
        {renderGrid('Trade Fees', data3, selection3, setSelection3)}
      </Grid>
    </>
  );
};

export default ReviewMatches;