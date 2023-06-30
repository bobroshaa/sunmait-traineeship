import Navbar from "../../components/Navbar/Navbar";
import New from "../../components/NewCollection/New";
import ProductSection from "../../components/ProductSection/ProductSection";
import "./home.css";
import React, { useEffect, useState } from "react";
import axios from "axios";
import Footer from "../../components/Footer/Footer";

const Home = () => {
  const [products, setProducts] = useState();
  const brandId = 2;

  useEffect(() => {
    const getProductsByBrand = async (brandId) => {
      try {
        const response = await axios.get(
          `http://localhost:5051/api/products/brands/${brandId}`
        );
        console.log("PRODUCTS", response.data);
        setProducts(response.data);
      } catch (error) {
        console.error(`Error: ${error}`);
      }
    };

    getProductsByBrand(brandId);
  }, []);

  return (
    <div>
      <New />
      {products && <ProductSection products={products} />}
      <Footer />
    </div>
  );
};

export default Home;
