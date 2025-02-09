import axios from 'axios';
import { loginFailure, loginStart, loginSuccess, registerFailure, registerStart, registerSuccess, logoutStart, logoutSuccess, logoutFailure } from '../store/authSlice';
// import { deleteUserFailure, deleteUserStart, deleteUserSuccess, getUsersFailure, getUsersStart, getUsersSuccess } from './userSlice';

import Cookies from "js-cookie";
import axiosInstanceInterceptor from "./axiosInstance";

const API_BASE_URL = "https://localhost:7085";
const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
    headers: { "Content-Type": "application/json" }
});

const handleError = (error, dispatch, failureAction) => {
    console.error(error.response ? error.response.data : error.message);
    dispatch(failureAction());
};

export const loginUser = async (user, dispatch, navigate) => {
    dispatch(loginStart());
    try {
        const res = await axiosInstance.post("/api/ArticleAppUser/Login", user);
        dispatch(loginSuccess(res.data));

        if (res.data.statusCode === 200) {
            const { accessToken, refreshToken } = res.data;
            Cookies.set("accessToken", accessToken, { secure: true, sameSite: "Strict" });
            Cookies.set("refreshToken", refreshToken, { secure: true, sameSite: "Strict" });
            navigate("/");
        } else {
            navigate("/login");
        }
    } catch (err) {
        handleError(err, dispatch, loginFailure);
    }
};

export const registerUser = async (user, dispatch, navigate) => {
    dispatch(registerStart());
    try {
        const res = await axiosInstance.post('/api/ArticleAppUser/Register', user);
        console.log(user)
        dispatch(registerSuccess(res.data));
        if (res.statusCode === 200) {
            navigate("/login");
        }
    } catch (err) {
        handleError(err, dispatch, registerFailure);
    }
};

// logout
// export const logout = async (token, dispatch, navigate) => {
//     dispatch(logoutStart());

//     try {
//         const res = await axiosInstance.post("/api/User/Logout", token);
//         console.log(res.data)
//         dispatch(logoutSuccess(res.data));
//         if (res.data.statusCode === 200) {
//             Cookies.remove("accessToken");
//             Cookies.remove("refreshToken");
//             navigate("/login");
//         }
//     } catch (err) {
//         handleError(err, dispatch, logoutFailure);
//     }
// };

// // Lấy tất cả người dùng
// export const getAllUsers = async (dispatch) => {
//     dispatch(getUsersStart());
//     try {
//         const res = await axiosInstanceInterceptor.get("/api/User/GetAllUser");
//         dispatch(getUsersSuccess(res.data));
//     } catch (err) {
//         handleError(err, dispatch, getUsersFailure);
//     }
// };

// // Xóa người dùng
// export const deleteUser = async (dispatch, id) => {
//     dispatch(deleteUserStart());
//     try {
//         const res = await axiosInstanceInterceptor.delete(`/api/User/Delete/${id}`);
//         dispatch(deleteUserSuccess(res.data));
//     } catch (err) {
//         handleError(err, dispatch, deleteUserFailure);
//     }
// };
