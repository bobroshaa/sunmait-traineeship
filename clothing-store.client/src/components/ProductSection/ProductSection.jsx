import React from "react";
import ProductCard from "../ProductCard/ProductCard";
import "./productSection.css";

const ProductSection = ({ products }) => {
  return (
    <div className="featured-product-section">
      <div className="featured-product-container">
        {products.map((product, index) => (
          <ProductCard
            key={index}
            product={product}
          />
        ))}
      </div>
    </div>
  );
};

export default ProductSection;
