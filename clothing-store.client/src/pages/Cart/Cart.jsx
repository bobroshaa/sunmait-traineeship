import { useEffect, useState, useRef } from "react";
import React from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import "./cart.css";
import { useToast } from "rc-toastr";
import { Add, CheckCircleOutline, Close, Remove } from "@mui/icons-material";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const Cart = () => {
  const [cartItems, setCartItems] = useState();
  const [connection, setConnection] = useState();
  const cartItemsRef = useRef(cartItems);

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

  const leaveRoom = async () => {
    try {
      if (connection) {
        cartItemsRef.current.forEach(async (item) => {
          await connection.invoke(
            "LeaveRoomFromCart",
            parseInt(item.productID),
            userId
          );
        });
        connection.stop();
      }
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const order = async () => {
    console.log(cartItems.map((item) => item.id));
    try {
      await axios.post(`http://localhost:5051/api/orders`, {
        cartItems,
      });

      showSuccessfulOrder();
    } catch (error) {
      showErrorOrder();
      console.error(`Error: ${error}`);
    }
  };

  const deleteCartItem = async (cartItemId) => {
    try {
      await axios.delete(`http://localhost:5051/api/cart/${cartItemId}`);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const joinRoom = async () => {
    if (connection) return;
    try {
      const hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5051/producthub")
        .configureLogging(LogLevel.Information)
        .build();

      hubConnection.on("updatequantity", (cartItem) => {
        console.log("BEFORE UPDATE QUANTITY", cartItemsRef.current);
        const updatedCartItems = cartItemsRef.current.map((item) => {
          if (item.id == cartItem.id) {
            return cartItem;
          }
          return item;
        });
        setCartItems(updatedCartItems);
      });

      hubConnection.on("updatecart", async () => {
        console.log("UPDATE CART");
        await getCartItems();
      });

      await hubConnection.start();
      cartItemsRef.current.forEach(async (item) => {
        console.log(parseInt(item.productID), userId);
        await hubConnection.invoke(
          "JoinRoomFromCart",
          parseInt(item.productID),
          parseInt(userId)
        );
      });

      setConnection(hubConnection);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const getCartItems = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5051/api/cart/${userId}`
      );
      setCartItems(response.data);
      console.log("GET CART ITEMS", response.data);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  useEffect(() => {
    getCartItems();
  }, []);

  useEffect(() => {
    cartItemsRef.current = cartItems;
    if (cartItems) {
      joinRoom();
    }
  }, [cartItems]);

  useEffect(() => {
    const handleBeforeUnload = async (e) => {
      e.preventDefault();
      await leaveRoom();
    };

    window.addEventListener("beforeunload", handleBeforeUnload);

    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
    };
  }, [connection]);

  const handleQuantityChange = async (itemId, newQuantity) => {
    try {
      await axios.put(
        `http://localhost:5051/api/cart/${itemId}`,
        JSON.parse(newQuantity),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

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
                <div className="cart-item-card" key={cartItem.id}>
                  <img
                    className="cart-item-image"
                    src={cartItem.imageURL}
                    alt={cartItem.name}
                  />
                  <div className="cart-item-card-info">
                    <span className="cart-item-price">
                      ${cartItem.price}{" "}
                      <Close
                        className="close-btn"
                        onClick={() => deleteCartItem(cartItem.id)}
                      />
                    </span>
                    <span className="cart-item-name">{cartItem.name}</span>
                    <span className="cart-item-size">
                      Size: One Size (M - L)
                    </span>
                    <div className="cart-available-quantity">
                      <CheckCircleOutline />{" "}
                      {cartItem.inStockQuantity - cartItem.reservedQuantity}{" "}
                      Items are Available
                    </div>
                    <div className="cart-item-quantity">
                      <button
                        className="cart-item-change-one"
                        onClick={() =>
                          handleQuantityChange(
                            cartItem.id,
                            cartItem.quantity - 1
                          )
                        }
                        disabled={cartItem.quantity <= 1}
                      >
                        <Remove fontSize={"small"} />
                      </button>
                      {cartItem.quantity}
                      <button
                        className="cart-item-change-one"
                        onClick={() =>
                          handleQuantityChange(
                            cartItem.id,
                            cartItem.quantity + 1
                          )
                        }
                        disabled={
                          cartItem.quantity >= 10 ||
                          cartItem.quantity ==
                            cartItem.inStockQuantity -
                              cartItem.reservedQuantity +
                              cartItem.quantity
                        }
                      >
                        <Add fontSize={"small"} />
                      </button>
                    </div>
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
                {cartItems.reduce(
                  (total, item) => total + item.price * item.quantity,
                  0
                )}
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
