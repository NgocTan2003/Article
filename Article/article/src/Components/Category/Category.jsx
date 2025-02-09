import { useState, useEffect } from "react";
import { NavLink, useLocation } from "react-router-dom";
import "./Category.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHouse } from "@fortawesome/free-solid-svg-icons";
import { getAllCategory } from "../../services/categoryService";

function Navbar() {
  const navLinkActive = (e) => {
    return e.isActive
      ? "category__link category__link--active"
      : "category__link";
  };

  const location = useLocation();
  const [listCategory, setListCategory] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await getAllCategory();
        const sortedData = data.sort((a, b) => a.displayOrder - b.displayOrder);
        setListCategory(sortedData);
      } catch (err) {
        console.error("Error fetching categories:", err);
      }
    };

    fetchCategories();
  }, []);

  return (
    <div className="category">
      <div className="container">
        <ul>
          <li>
            <NavLink to="/" className={navLinkActive}>
              <FontAwesomeIcon icon={faHouse} className="logoHome" />
            </NavLink>
          </li>

          {listCategory.map((item) => (
            <li key={item.id}>
              <NavLink
                className={(e) =>
                  `${navLinkActive(e)} ${
                    location.pathname.includes(`/${item.seoKeyword}`)
                      ? "category__link--active"
                      : ""
                  }`
                }
                to={`/${item.seoKeyword}`}
                end
              >
                {item.name}
              </NavLink>
              {item.articleSubCategories &&
                item.articleSubCategories.length > 0 && (
                  <ul className="menu__sub">
                    {item.articleSubCategories.map((sub) => (
                      <li key={sub.id}>
                        <NavLink
                          to={`/${item.seoKeyword}/${sub.seoKeyword}`}
                          className={navLinkActive}
                        >
                          {sub.name}
                        </NavLink>
                      </li>
                    ))}
                  </ul>
                )}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default Navbar;
