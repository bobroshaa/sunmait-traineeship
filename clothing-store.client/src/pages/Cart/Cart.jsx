import { useEffect, useState, useRef } from "react";
import React from "react";
import axios from "../../axiosConfig";
import { useParams } from "react-router-dom";
import "./cart.css";
import { useToast } from "rc-toastr";
import { Add, CheckCircleOutline, Close, Remove } from "@mui/icons-material";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Navbar from "../../components/Navbar/Navbar";

const Cart = () => {
  const [cartItems, setCartItems] = useState();
  const [connection, setConnection] = useState();
  const [user, setUser] = useState();

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
  const token = JSON.parse(localStorage.getItem("user")).accessToken;

  const joinRoom = async () => {
    if (connection) return;
    try {
      const hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5051/producthub")
        .configureLogging(LogLevel.Information)
        .build();

      hubConnection.on(
        "updateReservedQuantity",
        (reservedQuantity, productId) => {
          setCartItems((prevCartItems) => {
            const updatedCartItems = prevCartItems.map((item) => {
              if (item.productID === productId) {
                return {
                  ...item,
                  product: { ...item.product, reservedQuantity },
                };
              }
              return item;
            });
            return updatedCartItems;
          });
        }
      );

      hubConnection.on(
        "updateInStockQuantity",
        (inStockQuantity, productId) => {
          setCartItems((prevCartItems) => {
            const updatedCartItems = prevCartItems.map((item) => {
              if (item.productID === productId) {
                return {
                  ...item,
                  product: { ...item.product, inStockQuantity },
                };
              }
              return item;
            });
            return updatedCartItems;
          });
        }
      );

      hubConnection.on("updateCartItemQuantity", (cartItemId, quantity) => {
        setCartItems((prevCartItems) => {
          const updatedCartItems = prevCartItems.map((item) => {
            if (item.id === cartItemId) {
              return { ...item, quantity };
            }
            return item;
          });
          return updatedCartItems;
        });
      });

      hubConnection.on("updateCart", async () => {
        await getCartItems();
      });

      await hubConnection.start();

      await hubConnection.invoke(
        "JoinRoomFromCart",
        cartItemsRef.current.map((item) => parseInt(item.productID)),
        parseInt(userId)
      );

      setConnection(hubConnection);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const leaveRoom = async () => {
    try {
      if (connection) {
        await connection.invoke(
          "LeaveRoomFromCart",
          cartItemsRef.current.map((item) => parseInt(item.productID)),
          parseInt(userId)
        );

        connection.stop();
      }
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const order = async () => {
    try {
      const orderData = cartItems.map((item) => ({
        ...item,
        product: {
          ...item.product,
          brandName:
            item.product.brandName == null ? "" : item.product.brandName,
        },
      }));
      await axios.post(
        `http://localhost:5051/api/orders`,
        {
          cartItems: orderData,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      showSuccessfulOrder();
    } catch (error) {
      showErrorOrder();
      console.error(`Error: ${error}`);
    }
  };

  const deleteCartItem = async (cartItemId) => {
    try {
      await axios.delete(`http://localhost:5051/api/cart/${cartItemId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const getCartItems = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5051/api/cart/${userId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setCartItems(() => {
        return response.data;
      });
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const getUser = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5051/api/users/${userId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setUser(response.data);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  useEffect(() => {
    getCartItems();
    getUser();
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
      await axios.put(`http://localhost:5051/api/cart/${itemId}`, newQuantity, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  return (
    <div>
      {user && <Navbar firstName={user.firstName} />}
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
                      src={cartItem.product.imageURL}
                      alt={cartItem.product.name}
                    />
                    <div className="cart-item-card-info">
                      <span className="cart-item-price">
                        ${cartItem.product.price / 100}{" "}
                        <Close
                          className="close-btn"
                          onClick={() => deleteCartItem(cartItem.id)}
                        />
                      </span>
                      <span className="cart-item-name">
                        {cartItem.product.name}
                      </span>
                      <span className="cart-item-size">
                        Size: One Size (M - L)
                      </span>
                      <div className="cart-available-quantity">
                        <CheckCircleOutline />{" "}
                        {cartItem.product.inStockQuantity -
                          cartItem.product.reservedQuantity}{" "}
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
                            cartItem.quantity ==
                            cartItem.product.inStockQuantity -
                              cartItem.product.reservedQuantity +
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
                    (total, item) => total + item.product.price * item.quantity,
                    0
                  ) / 100}
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
    </div>
  );
};

export default Cart;
