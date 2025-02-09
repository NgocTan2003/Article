import { useState } from "react";
import "./Register.scss";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { registerUser } from "../../../services/authService";
import { useSelector } from "react-redux";

const Register = () => {
  const [userName, setUserName] = useState("");
  const [passWord, setPassWord] = useState("");
  const [confirmPassWord, setConfirmPassWord] = useState("");
  const [email, setEmail] = useState("");
  const [fullName, setFullName] = useState("");
  const [gender, setGender] = useState("0");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [role, setRole] = useState("Customer");
  const [errors, setErrors] = useState({});

  const register = useSelector((state) => state.auth.register.response);

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const validateField = (name, value) => {
    const newErrors = { ...errors };

    if (name === "fullName" && value.length < 6) {
      newErrors.fullName = "Fullname phải có ít nhất 6 ký tự.";
    } else if (name === "fullName") {
      delete newErrors.fullName;
    }

    if (name === "userName" && value.length < 6) {
      newErrors.userName = "Username phải có ít nhất 6 ký tự.";
    } else if (name === "userName") {
      delete newErrors.userName;
    }

    if (name === "phoneNumber" && !/^\d{10}$/.test(value)) {
      newErrors.phoneNumber = "Số điện thoại phải có đúng 10 ký tự.";
    } else if (name === "phoneNumber") {
      delete newErrors.phoneNumber;
    }

    if (
      name === "passWord" &&
      !/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}/.test(
        value
      )
    ) {
      newErrors.passWord =
        "Mật khẩu phải có ít nhất 6 ký tự, gồm 1 ký tự viết thường, 1 ký tự viết hoa, 1 ký tự đặc biệt và 1 chữ số.";
    } else if (name === "passWord") {
      delete newErrors.passWord;
    }

    if (name === "email" && !value.includes("@")) {
      newErrors.email = "Email không hợp lệ.";
    } else if (name === "email") {
      delete newErrors.email;
    }

    setErrors(newErrors);
  };

  const handleChange = (name, value) => {
    if (name === "userName") setUserName(value);
    if (name === "passWord") setPassWord(value);
    if (name === "confirmPassWord") setConfirmPassWord(value);
    if (name === "email") setEmail(value);
    if (name === "fullName") setFullName(value);
    if (name === "phoneNumber") setPhoneNumber(value);

    validateField(name, value);
  };

  const handleRegister = (e) => {
    e.preventDefault();
    if (Object.keys(errors).length > 0) {
      return;
    }

    const newUser = {
      userName: userName,
      passWord: passWord,
      confirmPassWord: confirmPassWord,
      email: email,
      fullName: fullName,
      gender: gender,
      phoneNumber: phoneNumber,
      role: role,
    };
    registerUser(newUser, dispatch, navigate);
  };

  return (
    <section className="register-container">
      <div className="register-title">Sign up</div>
      <form onSubmit={handleRegister}>
        <label>FULLNAME</label>
        <input
          type="text"
          placeholder="Enter your fullname"
          value={fullName}
          onChange={(e) => handleChange("fullName", e.target.value)}
        />
        {errors.fullName && <span className="error">{errors.fullName}</span>}

        <label>GENDER</label>
        <select value={gender} onChange={(e) => setGender(e.target.value)}>
          <option value="0">Male</option>
          <option value="1">Female</option>
          <option value="2">Other</option>
        </select>

        <label>PHONENUMBER</label>
        <input
          type="phone"
          placeholder="Enter your phone"
          value={phoneNumber}
          onChange={(e) => handleChange("phoneNumber", e.target.value)}
        />
        {errors.phoneNumber && (
          <span className="error">{errors.phoneNumber}</span>
        )}

        <label>EMAIL</label>
        <input
          type="email"
          placeholder="Enter your email"
          value={email}
          onChange={(e) => handleChange("email", e.target.value)}
        />
        {errors.email && <span className="error">{errors.email}</span>}

        <label>USERNAME</label>
        <input
          type="text"
          placeholder="Enter your username"
          value={userName}
          onChange={(e) => handleChange("userName", e.target.value)}
        />
        {errors.userName && <span className="error">{errors.userName}</span>}

        <label>PASSWORD</label>
        <input
          type="password"
          placeholder="Enter your password"
          value={passWord}
          onChange={(e) => handleChange("passWord", e.target.value)}
        />
        {errors.passWord && <span className="error">{errors.passWord}</span>}

        <label>CONFIRM PASSWORD</label>
        <input
          type="password"
          placeholder="Enter your confirm password"
          value={confirmPassWord}
          onChange={(e) => handleChange("confirmPassWord", e.target.value)}
        />
        <button type="submit">Create account</button>
        {register && <span className="text-danger">{register.message}</span>}
      </form>
    </section>
  );
};

export default Register;
