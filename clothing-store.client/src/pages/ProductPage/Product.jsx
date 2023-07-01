import { useEffect, useState } from "react";
import React from "react";
import axios from "../../axiosConfig";
import { useParams } from "react-router-dom";
import "./product.css";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Accordion from "../../components/Accordion/Accordion";
import {
  AccessTimeOutlined,
  CheckCircleOutline,
  LocalShippingOutlined,
  Repeat,
  VisibilityOutlined,
} from "@mui/icons-material";
import { useToast } from "rc-toastr";
import Navbar from "../../components/Navbar/Navbar";

const Product = () => {
  const [product, setProduct] = useState();
  const [connection, setConnection] = useState();
  const [viewersCount, setViewersCount] = useState();

  const { toast } = useToast();

  const showSuccessfulNotification = () => {
    toast("Product successfully added to your cart!");
  };

  const showErrorNotification = () => {
    toast("We're sorry, but there was an error processing your request.");
  };

  const params = useParams();
  const productId = params.productId;

  const userId = JSON.parse(localStorage.getItem("user")).id;
  const token = JSON.parse(localStorage.getItem("user")).accessToken;

  const joinRoom = async () => {
    if (connection) return;
    console.log(product);
    try {
      const hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5051/producthub")
        .configureLogging(LogLevel.Information)
        .build();

      hubConnection.on("updateViewers", (message) => {
        setViewersCount(message);
      });

      hubConnection.on("updateReservedQuantity", (reservedQuantity) => {
        console.log("GROUP?: ", hubConnection.connection.features.groups);
        setProduct((prev) => ({
          ...prev,
          reservedQuantity,
        }));
      });

      hubConnection.on("updateInStockQuantity", (inStockQuantity) => {
        console.log("GROUP?: ", hubConnection.connectionId);
        setProduct((prev) => ({
          ...prev,
          inStockQuantity,
        }));
      });

      hubConnection.onclose(() => {
        leaveRoom(productId);
      });

      await hubConnection.start();
      await hubConnection.invoke(
        "JoinRoomFromProduct",
        parseInt(productId),
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
          "LeaveRoomFromProduct",
          parseInt(productId),
          parseInt(userId)
        );
        connection.stop();
      }
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const getProduct = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5051/api/products/${productId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      setProduct(response.data);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  useEffect(() => {
    getProduct();
  }, []);

  useEffect(() => {
    if (product !== undefined) {
      joinRoom();
    }
  }, [product]);

  useEffect(() => {
    const handleBeforeUnload = async (e) => {
      e.preventDefault();
      await leaveRoom();
    };

    window.addEventListener("beforeunload", handleBeforeUnload);

    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
      if (connection) {
        leaveRoom();
      }
    };
  }, [connection]);

  const addToCart = async () => {
    try {
      const response = await axios.post(
        `http://localhost:5051/api/cart`,
        {
          quantity: 1,
          productID: productId,
          userID: userId,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      console.log(response);
      showSuccessfulNotification();
    } catch (error) {
      showErrorNotification();
      console.error(`Error: ${error}`);
    }
  };

  return (
    <div>
      <Navbar />
      <div className="product-page-container">
        {product && (
          <div className="product-container">
            <img
              src={product.imageURL}
              alt={product.name}
              className="product-image"
            />

            <div className="product-details">
              <h3 className="product-name">{product.name}</h3>
              {product.brand && (
                <div className="product-price">{product.brand}</div>
              )}
              <div className="product-price">${product.price}</div>
              <div className="product-size">Size: One Size (M - L)</div>

              <div className="extra-info-container">
                <LocalShippingOutlined />
                <div className="extra-info-text-container">
                  <span className="extra-info-header">Fast Delivery</span>
                  <span className="extra-info-description">
                    Processing and delivery of the order is carried out within
                    2-3 working days.
                  </span>
                </div>
              </div>

              <div className="extra-info-container">
                <Repeat />
                <div className="extra-info-text-container">
                  <span className="extra-info-header">Easy Returns</span>
                  <span className="extra-info-description">
                    Exchange or return your product(s) within 14 days.
                  </span>
                </div>
              </div>

              <Accordion title="Description" content={product.description} />
              <div className="product-viewers">
                <VisibilityOutlined /> {viewersCount} Viewers
              </div>
              <div className="product-viewers">
                <AccessTimeOutlined /> {product.reservedQuantity} Items were
                already Reserved
              </div>
              <div className="product-viewers">
                <CheckCircleOutline />{" "}
                {product.inStockQuantity - product.reservedQuantity} Items are
                Available
              </div>

              {product.inStockQuantity - product.reservedQuantity > 0 ? (
                <button onClick={addToCart} className="add-to-cart">
                  Add to Cart
                </button>
              ) : (
                <button disabled className="out-of-stock">
                  Out Of Stock
                </button>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default Product;
