import "../assets/styles/components/navbar.css";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import Logo from "../assets/images/cwd-logo.png";
import { MdMenu } from "react-icons/md";
import { MdClose } from "react-icons/md";

export default function Navbar() {
  const [open, setOpen] = useState(false);
  return (
    <div className="navbar">
      <div className="container nav-wrapper">
        <div className="logo">
          <Link to="/">
            <img src={Logo} alt="cwd logo" />
          </Link>
        </div>
        <div className={open ? "nav-links active" : "nav-links"}>
          <Link to="/" onClick={() => setOpen(false)}>
            <span>Home</span>
          </Link>
          <Link to="/courses" onClick={() => setOpen(false)}>
            <span>Courses</span>
          </Link>
          <Link to="#topics" onClick={() => setOpen(false)}>
            <span>Topics</span>
          </Link>
          <Link to="#contact" onClick={() => setOpen(false)}>
            <span>Contact</span>
          </Link>
          <Link to="/login" onClick={() => setOpen(false)}>
            <span className="login-btn">Login</span>
          </Link>
        </div>
        <div className="menu-btn" onClick={() => setOpen((prev) => !prev)}>
          {open ? (
            <MdClose className="menu-icon" />
          ) : (
            <MdMenu className="menu-icon" />
          )}
        </div>
      </div>
    </div>
  );
}
