import "./login.css";
import React from "react";
import axios from "../../axiosConfig";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [loginData, setLoginData] = useState({});

  const navigate = useNavigate();

  const updateLoginData = (e) => {
    setLoginData({
      ...loginData,
      [e.target.name]: e.target.value,
    });
  };

  const login = async (e) => {
    e.preventDefault();
    try {
      console.log("LOGIN DATA", loginData);
      const response = await axios.post(
        "http://localhost:5051/api/authentication",
        {
          email: loginData.email,
          password: loginData.password,
        }
      );
      console.log(response.data);
      localStorage.setItem("user", JSON.stringify(response.data));
      navigate("/");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="login-page-container">
      <div className="container">
        <img
          className="image-side"
          src="https://i.pinimg.com/564x/a6/a8/a0/a6a8a0699ba1e63db8bb5068dee6605c.jpg"
          alt="Decorative image"
        />
        <div className="login-side">
          <h3 className="login-side-header">Sign In to continue.</h3>
          <div className="input-container">
            <label className="label" for="email">
              Email:
            </label>
            <input
              className="input"
              type="email"
              id="email"
              name="email"
              required
              placeholder="Enter email"
              onChange={updateLoginData}
            />
          </div>
          <div className="input-container">
            <label className="label" for="password">
              Password:
            </label>
            <input
              className="input"
              type="password"
              id="password"
              name="password"
              required
              placeholder="Enter password"
              onChange={updateLoginData}
            />
          </div>
          <span className="error-text" id="error-text">
            Wrong email or password.
          </span>
          <div>
            <button onClick={login} className="login-button" type="submit">
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
