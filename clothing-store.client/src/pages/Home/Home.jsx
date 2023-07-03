import New from "../../components/NewCollection/New";
import ProductSection from "../../components/ProductSection/ProductSection";
import "./home.css";
import React, { useEffect, useState } from "react";
import axios from "../../axiosConfig";
import Footer from "../../components/Footer/Footer";
import Navbar from "../../components/Navbar/Navbar";

const Home = () => {
  const [products, setProducts] = useState();
  const [user, setUser] = useState();
  const brandId = 2;

  const token = JSON.parse(localStorage.getItem("user")).accessToken;
  const userId = JSON.parse(localStorage.getItem("user")).id;

  useEffect(() => {
    const getProductsByBrand = async (brandId) => {
      try {
        const response = await axios.get(
          `http://localhost:5051/api/products/brands/${brandId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        console.log("PRODUCTS", response.data);
        setProducts(response.data);
      } catch (error) {
        console.error(`Error: ${error}`);
      }
    };

    const getUser = async (userId) => {
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

    getProductsByBrand(brandId);
    getUser(userId);
  }, []);

  return (
    <div>
      {user && <Navbar firstName={user.firstName} />}
      <New />
      {products && <ProductSection products={products} />}
      <Footer />
    </div>
  );
};

export default Home;
