import "./App.css";
import React from "react";
import Product from "./pages/ProductPage/Product";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Cart from "./pages/Cart/Cart";
import Login from "./pages/Login/Login";
import Home from "./pages/Home/Home";
import Navbar from "./components/Navbar/Navbar";

const App = () => {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/products/:productId" element={<Product />} />
        <Route path="/cart/:userId" element={<Cart />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
