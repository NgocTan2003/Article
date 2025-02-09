import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import "./LayoutDefault.scss";
// import Header from "../../pages/Header/Header";
import Category from "../../Components/Category/Category";
import Footer from "../../Components/Footer/Footer";
import Navbar from "../../Components/Navbar/Navbar";

function LayoutDefault() {
  const [scrollY, setScrollY] = useState(0); // Vị trí cuộn hiện tại
  const [isNavbarSticky, setIsNavbarSticky] = useState(false); // Xác định trạng thái của Navbar

  useEffect(() => {
    const handleScroll = () => {
      const currentScroll = window.scrollY;
      setScrollY(currentScroll);

      // Kiểm tra vị trí để chuyển đổi trạng thái Navbar
      if (currentScroll > 58) {
        // Giá trị 58 là vị trí <Header /> ban đầu
        setIsNavbarSticky(true);
      } else {
        setIsNavbarSticky(false);
      }
    };

    window.addEventListener("scroll", handleScroll);

    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  return (
    <>
      <div
        className={`layout-default__navbar ${isNavbarSticky ? "hidden" : ""}`}
      >
        <Navbar />
      </div>
      <div
        className={`layout-default__category ${isNavbarSticky ? "sticky" : ""}`}
      >
        <Category />
      </div>

      <main className="layout-default__main">
        <div style={{ height: 2000 }}>
          <Outlet />
        </div>
      </main>

      <div className="layout-default__footer">
        <Footer />
      </div>
    </>
  );
}

export default LayoutDefault;
