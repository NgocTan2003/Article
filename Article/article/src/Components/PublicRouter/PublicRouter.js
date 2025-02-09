import { Navigate } from "react-router-dom";
import Cookies from "js-cookie";

const PublicRoute = ({ children }) => {
  const isTokenExpired = (token) => {
    if (!token) return true; // Token does not exist => hết hạn
    const [, payload] = token.split(".");
    const decoded = JSON.parse(atob(payload));
    return decoded.exp * 1000 < Date.now(); // Token hết hạn
  };
  
  const accessToken = Cookies.get("accessToken");
  const isAuthenticated = accessToken && !isTokenExpired(accessToken);

  return <>{isAuthenticated ? <Navigate to="/" replace /> : children}</>;
};

export default PublicRoute;
