import React from 'react';
import { Routes, Route, Link } from 'react-router-dom';
import { CssBaseline, Drawer, List, ListItem, ListItemText, Toolbar, AppBar, Typography, Box } from '@mui/material';
import Home from './Pages/Home';
import Contact from './Pages/Contact';
import TransactionPendingMatches from './Pages/Transactions/PendingMatches';
import TransactionReviewMatches from './Pages/Transactions/ReviewMatches';
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
      </Routes>
    </Layout>
  );
};

export default App;