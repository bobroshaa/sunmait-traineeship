import './App.css';
import React from 'react';
import Product from './pages/ProductPage/Product';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import Cart from './pages/Cart/Cart';

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/products/:productId"
          element={<Product />}
        />
      </Routes>
      <Routes>
        <Route
          path="/cart/:userId"
          element={<Cart />}
        />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
