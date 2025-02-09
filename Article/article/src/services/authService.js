import axiosInstance from "../utils/axiosInstance";
import Cookies from "js-cookie";
import { axiosPublic, axiosPrivate } from "../utils/axiosCustomize";
import {
  loginFailure,
  loginStart,
  loginSuccess,
  registerFailure,
  registerStart,
  registerSuccess,
  logoutStart,
  logoutSuccess,
  logoutFailure,
} from "../store/authSlice";

export const loginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axiosPublic.post("/api/ArticleAppUser/Login", user);
    dispatch(loginSuccess(res));

    if (res.statusCode === 200) {
      Cookies.set("accessToken", res.accessToken, {
        secure: true,
        sameSite: "Strict",
      });
      Cookies.set("refreshToken", res.refreshToken, {
        secure: true,
        sameSite: "Strict",
      });
      navigate("/");
    } else {
      navigate("/login");
    }
  } catch (err) {
    console.error(err.response ? err.response.data : err.message);
    dispatch(loginFailure());
  }
};

export const registerUser = async (user, dispatch, navigate) => {
  dispatch(registerStart());
  try {
    const res = await axiosPublic.post("/api/ArticleAppUser/Register", user);
    dispatch(registerSuccess(res));
    if (res.statusCode === 200) {
      const sendEmail = await confirmEmail(user.email);
      console.log("sendEmail", sendEmail);
      navigate("/login");
    }
  } catch (err) {
    console.error(err.response ? err.response.data : err.message);
    dispatch(registerFailure());
  }
};

export const confirmEmail = async (email) => {
  try {
    const res = await axiosPublic.get("/api/ArticleAppUser/SendEmailConfirm", {
      params: { email: email },
    });
    return res;
  } catch (err) {
    console.error(err.response ? err.response.data : err.message);
  }
};

export const logout = async (token, dispatch, navigate) => {
  dispatch(logoutStart());
  try {
    const res = await axiosInstance.post("/api/User/Logout", token);
    dispatch(logoutSuccess(res.data));
    if (res.data.statusCode === 200) {
      Cookies.remove("accessToken");
      Cookies.remove("refreshToken");
      navigate("/login");
    }
  } catch (err) {
    console.error(err.response ? err.response.data : err.message);
    dispatch(logoutFailure());
  }
};
