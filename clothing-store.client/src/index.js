import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { ToastProvider } from "rc-toastr";
import "rc-toastr/dist/index.css";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <ToastProvider
    config={{
      position: "bottom-left",
      duration: 3000,
    }}
  >
    <App />
  </ToastProvider>
);
