import { useEffect, useState } from "react";
import React from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import "./cart.css";

const Cart = () => {
  const [cartItems, setCartItems] = useState();
  const [connection, setConnection] = useState();

  const params = useParams();
  const userId = params.userId;

  const getCartItems = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5051/api/cart/${userId}`
      );
      setCartItems(response.data);
      console.log(response.data);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  useEffect(() => {
    getCartItems();
  }, []);

  return (
    <div className="cart-page-container">
      {cartItems && (
        <div className="cart-container">
          <div className="cart-header">
            <span className="cart-name">My bag</span>
            <span className="reservation-info">Items are reserved for 20 minutes</span>
          </div>
          {cartItems.map((cartItem) => {
            return (
              <div className="cart-item-card">
                <img
                  className="cart-item-image"
                  src={cartItem.imageURL}
                  alt={cartItem.name}
                />
                <div className="cart-item-card-info">
                  <span className="cart-item-price">${cartItem.price}</span>
                  <span className="cart-item-name">{cartItem.name}</span>
                  <span className="cart-item-size">Size: One Size (M - L)</span>
                </div>
              </div>
            );
          })}
        </div>
      )}
    </div>
  );
};

export default Cart;
