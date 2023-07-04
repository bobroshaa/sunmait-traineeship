import { Person, ShoppingBag } from "@mui/icons-material";
import "./navbar.css";
import React from "react";
import { Link } from "react-router-dom";

const Navbar = ({ firstName }) => {
  const userId = JSON.parse(localStorage.getItem("user")).id;

  return (
    <div className="navbar-container">
      <div className="navbar-right-side">
        <Link to={"/"} className="navbar-logo">
          Logo
        </Link>
        <span className="navbar-item">Women</span>
        <span className="navbar-item">Men</span>
      </div>
      <div className="navbar-left-side">
        <span className="navbar-item">Hi, {firstName}!</span>
        <span className="navbar-item">
          <Person />
        </span>
        <Link to={`/cart/${userId}`} className="navbar-item">
          <ShoppingBag />
        </Link>
      </div>
    </div>
  );
};

export default Navbar;
