import './App.css';
import React from 'react';
import Product from './pages/ProductPage/Product';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/products/:productId"
          element={<Product />}
        />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
