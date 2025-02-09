import "./Login.scss";
import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import { loginUser } from "../../../services/authService";
import { useDispatch } from "react-redux";
import { useSelector } from "react-redux";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const element = <h1>Hello, world!</h1>;

  const user = useSelector((state) => state.auth.login.currentUser);

  const handleLogin = (e) => {
      e.preventDefault();
      const newUser = {
          username: username,
          password: password
      }
      loginUser(newUser, dispatch, navigate);
  }

  return (
    <section className="login-container">
      <div className="login-title"> Log in {element}</div>
            <form onSubmit={handleLogin}>
                <label>USERNAME</label>
                <input type="text"
                    placeholder="Enter your username"
                    onChange={(e) => setUsername(e.target.value)}
                />
                <label>PASSWORD</label>
                <input type="password"
                    placeholder="Enter your password"
                    onChange={(e) => setPassword(e.target.value)}
                />
                <button type="submit"> Login </button>

                {user && <span className="text-danger">{user.message}</span>}
            </form>
            <div className="login-register"> Don't have an account yet? </div>
            <Link className="login-register-link" to="/register">Register one for free </Link>
      <h2>Login</h2>
    </section>
  );
};

export default Login;
