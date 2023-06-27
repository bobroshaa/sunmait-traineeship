import { useEffect, useState } from "react";
import React from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import "./cart.css";
import { useToast } from "rc-toastr";

const Cart = () => {
  const [cartItems, setCartItems] = useState();

  const { toast } = useToast();

  const showSuccessfulOrder = () => {
    toast(
      "Thank you for the order! It has already been submitted for processing."
    );
  };

  const showErrorOrder = () => {
    toast("We're sorry, but there was an error processing your request.");
  };

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

  const order = async () => {
    console.log(cartItems.map((item) => item.id));
    try {
      const response = await axios.post(`http://localhost:5051/api/orders`, {
        userID: 1,
        cartItemIds: cartItems.map((item) => item.id),
      });

      setCartItems();

      showSuccessfulOrder();
    } catch (error) {
      showErrorOrder();
      console.error(`Error: ${error}`);
    }
  };

  useEffect(() => {
    getCartItems();
  }, []);

  return (
    <div className="cart-page-container">
      {cartItems && cartItems.length > 0 ? (
        <div className="cart-oder-container">
          <div className="cart-container">
            <div className="cart-header">
              <span className="cart-name">My bag</span>
              <span className="reservation-info">
                Items are reserved for 20 minutes
              </span>
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
                    <span className="cart-item-size">
                      Size: One Size (M - L)
                    </span>
                  </div>
                </div>
              );
            })}
          </div>

          <div className="total-order-container">
            <div className="total-order-price">
              <div className="total-order-header">Total</div>
              <div className="order-subtotal-price">
                Sub-total: $
                {cartItems.reduce((total, item) => total + item.price, 0)}
              </div>
              <div className="order-delivery">Delivery: Free</div>
              <button onClick={order} className="add-to-cart">
                Order
              </button>
            </div>
          </div>
        </div>
      ) : (
        <div className="empty-cart-message">Your bag is empty.</div>
      )}
    </div>
  );
};

export default Cart;
