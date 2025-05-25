import React from 'react';
import { Routes, Route, Link } from 'react-router-dom';
import { CssBaseline, Drawer, List, ListItem, ListItemText, Toolbar, AppBar, Typography, Box } from '@mui/material';
import Home from './Pages/Home';
import Contact from './Pages/Contact';
import TransactionPendingMatches from './Pages/Transactions/PendingMatches';
import TransactionReviewMatches from './Pages/Transactions/ReviewMatches';
import BySnapshotStockPriceGroupId from './Pages/Analytics/BySnapshotStockPriceGroupId';
import BySnapshotSummaryPriceGroupId from './Pages/Analytics/BySnapshotSummaryPriceGroupId';
import ContactMe from './Pages/Analytics/ContactMe';
import './App.css';

const drawerWidth = 240;

const Layout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
        <Toolbar>
          <Typography variant="h6" noWrap component="div">
            My MUI App
          </Typography>
        </Toolbar>
      </AppBar>
      <Drawer
        variant="permanent"
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
        }}
      >
        <Toolbar />
        <Box sx={{ overflow: 'auto' }}>
          <List>
            <ListItem component={Link} to="/">
              <ListItemText primary="Home" />
            </ListItem>
            <ListItem component={Link} to="/about">
              <ListItemText primary="About" />
            </ListItem>
            <ListItem component={Link} to="/contact">
              <ListItemText primary="Contact" />
            </ListItem>
            <ListItem>
              <ListItemText primary="Transactions" />
            </ListItem>
            <ListItem component={Link} to="/transactions/pending-matches">
              <ListItemText primary="Pending Matches" />
            </ListItem>
            <ListItem>
              <ListItemText primary="Analytics" />
            </ListItem>
            <ListItem component={Link} to="/analytics/by-snapshot-stock-price-group-id/1">
              <ListItemText primary="Vs Snapshot" />
            </ListItem>
            <ListItem component={Link} to="/analytics/by-snapshot-summary-price-group-id/1">
              <ListItemText primary="Summary Snapshot" />
            </ListItem>
          </List>
        </Box>
      </Drawer>
      <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
        <Toolbar />
        {children}
      </Box>
    </Box>
  );
};

const App: React.FC = () => {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/transactions/pending-matches" element={<TransactionPendingMatches />} />
        <Route path="/transactions/review-matches/:buyerId/:stockId" element={<TransactionReviewMatches />} />
        <Route path="/analytics/contact-me" element={<ContactMe />} />
        <Route path="/analytics/by-snapshot-stock-price-group-id/:priceGroupId" element={<BySnapshotStockPriceGroupId />} />
        <Route path="/analytics/by-snapshot-summary-price-group-id/:priceGroupId" element={<BySnapshotSummaryPriceGroupId />} />
      </Routes>
    </Layout>
  );
};

export default App;