import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMagnifyingGlass, faUser } from "@fortawesome/free-solid-svg-icons";
import "./Navbar.scss";

function Header() {
  const [showMenu, setShowMenu] = useState(false);

  const today = new Date();
  const options = {
    weekday: "long",
    day: "numeric",
    month: "numeric",
    year: "numeric",
  };
  const formattedDate = today.toLocaleDateString("vi-VN", options);

  const toggleMenu = () => {
    setShowMenu((prev) => !prev);
  };

  const closeMenu = () => {
    setShowMenu(false);
  };

  return (
    <div className="header">
      <div className="container">
        <div className="d-flex justify-content-between align-items-center header__left">
          <div className="d-flex align-items-center header__left">
            <a className="logo me-3" href="/">
              <img
                src="https://s1.vnecdn.net/vnexpress/restruct/i/v9546/v2_2019/pc/graphics/logo-tet-2024.svg"
                alt="Logo"
                className="img-fluid"
              />
            </a>
            <span className="text-date">{formattedDate}</span>
          </div>
          <div className="d-flex align-items-center header__right">
            {showMenu && <div className="overlay" onClick={closeMenu}></div>}
            <div className="btn-user">
              <FontAwesomeIcon icon={faUser} onClick={toggleMenu} />
              {showMenu && (
                <div className="user-menu">
                  <a href="/login" className="menu-item">
                    Đăng nhập
                  </a>
                  <a href="/register" className="menu-item">
                    Đăng ký
                  </a>
                </div>
              )}
            </div>
            <button className="btn-search">
              <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Header;
