import { useEffect, useState } from "react";
import React from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import "./product.css";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Accordion from "../../components/Accordion/Accordion";
import { LocalShippingOutlined, Repeat, VisibilityOutlined } from "@mui/icons-material";

const Product = () => {
  const [product, setProduct] = useState();
  const [connection, setConnection] = useState();
  const [viewersCount, setViewersCount] = useState();

  const params = useParams();
  const productId = params.productId;

  const joinRoom = async (productId) => {
    try {
      const hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5051/producthub")
        .configureLogging(LogLevel.Information)
        .build();

      hubConnection.on("updateViewers", (message) => {
        setViewersCount(message);
      });

      await hubConnection.start();
      await hubConnection.invoke("JoinRoom", parseInt(productId));

      setConnection(hubConnection);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const leaveRoom = async (productId) => {
    try {
      if (connection) {
        await connection.invoke("LeaveRoom", parseInt(productId));
        connection.stop();
      }
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  const getProduct = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5051/api/products/${productId}`
      );
      setProduct(response.data);
    } catch (error) {
      console.error(`Error: ${error}`);
    }
  };

  useEffect(() => {
    getProduct();
    joinRoom(productId);
  }, []);

  useEffect(() => {
    const handleBeforeUnload = async (e) => {
      e.preventDefault();
      await leaveRoom(productId);
    };

    window.addEventListener("beforeunload", handleBeforeUnload);

    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
    };
  }, [connection]);

  return (
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
                  Processing and delivery of the order is carried out within 2-3
                  working days.
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
            <div className="product-viewers"><VisibilityOutlined /> {viewersCount} Viewers</div>
            <button className="add-to-cart">Add to Cart</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Product;
