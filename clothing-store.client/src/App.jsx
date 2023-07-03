import "./App.css";
import React from "react";
import Product from "./pages/ProductPage/Product";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Cart from "./pages/Cart/Cart";
import Login from "./pages/Login/Login";
import Home from "./pages/Home/Home";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route exact path="/" element={!JSON.parse(localStorage.getItem("user")) ? <Navigate to="/login" /> : <Home />} />
        <Route
          path="/products/:productId"
          element={!JSON.parse(localStorage.getItem("user")) ? <Navigate to="/login" /> : <Product />}
        />
        <Route
          path="/cart/:userId"
          element={!JSON.parse(localStorage.getItem("user")) ? <Navigate to="/login" /> : <Cart />}
        />
        <Route exact path="/login" element={!JSON.parse(localStorage.getItem("user")) ? <Login /> : <Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
