import React, { useEffect, useState } from 'react';
import { Typography } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { BASE_URL } from '../../config';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'Stock ID', width: 120 },
  { field: 'stockName', headerName: 'Stock Name', width: 480 },
  // { field: 'userId', headerName: 'User ID', hideable: true },
];

const PendingMatches: React.FC = () => {
  const [data, setData] = useState<any[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get<any[]>(`${BASE_URL}/TradeTransaction/get-unassigned-buy-sell-matches?buyerId=1`)
      .then(res => setData(res.data))
      .catch(err => console.error(err));
  }, []);

  const handleRowClick = (params: any) => {
    const { id: stockId, userId } = params.row;
    navigate(`/transactions/review-matches/${userId}/${stockId}`);
  };

  return (
    <>
      <div style={{ display: 'flex', flexDirection: 'column' }}>
        <Typography variant="h4" gutterBottom>Unassigned Stocks</Typography>
        <DataGrid
          sx={{
            '& .MuiDataGrid-columnHeaderTitle': {
              fontWeight: 'bold',
            }
          }}
          rows={data}
          columns={columns}
          getRowId={(row) => row.id}
          onRowClick={handleRowClick}
        />
      </div>
    </>
  );
};

export default PendingMatches;
