import React from 'react';
import './productCard.css';
import { Link } from 'react-router-dom';

//TODO: get brand from product
const ProductCard = ({ product }) => {
  return (
    <Link to={`products/${product.id}`}>
    <div className="product-card-container">
      <img src={product.imageURL} alt={product.name} className="product-card-image" />
      <div className="product-card-info">
        <div className="product-card-name">{product.name}</div>
        <span className="product-card-price">${product.price / 100}</span>
      </div>
      <div className='product-card-brand'>stone island</div>
    </div>
    </Link>
  );
};

export default ProductCard;