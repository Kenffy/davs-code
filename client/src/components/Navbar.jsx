import "../assets/styles/components/navbar.css";
import React from "react";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <div className="navbar">
      <div className="container nav-wrapper">
        <div className="logo">
          <Link to="/">
            <h2>CodeWithDavs</h2>
          </Link>
        </div>
        <div className="nav-links">
          <Link to="/courses">
            <span>Courses</span>
          </Link>
          <Link to="#topics">
            <span>Topics</span>
          </Link>
          <Link to="#contact">
            <span>Contact</span>
          </Link>
          <Link to="/login">
            <span>Login</span>
          </Link>
        </div>
      </div>
    </div>
  );
}
