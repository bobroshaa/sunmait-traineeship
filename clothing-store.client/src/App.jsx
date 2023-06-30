import "./App.css";
import React from "react";
import Product from "./pages/ProductPage/Product";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Cart from "./pages/Cart/Cart";
import Login from "./pages/Login/Login";
import Home from "./pages/Home/Home";
import Navbar from "./components/Navbar/Navbar";

const App = () => {
  const user = JSON.parse(localStorage.getItem("user"));

  return (
    <BrowserRouter>
      {user && <Navbar />}
      <Routes>
        <Route path="/" element={!user ? <Navigate to="/login" /> : <Home />} />
        <Route
          path="/products/:productId"
          element={!user ? <Navigate to="/login" /> : <Product />}
        />
        <Route
          path="/cart/:userId"
          element={!user ? <Navigate to="/login" /> : <Cart />}
        />
        <Route path="/login" element={<Login />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
