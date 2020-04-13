import React from "react";
import "../../App.css";
import { Link } from "react-router-dom";
import logo from "./logo_final.png";

export default function Logo() {
  return (
    <div className="container">
      <Link to="/">
        <img src={logo} alt="WhatsIn logo" />
      </Link>
    </div>
  );
}
