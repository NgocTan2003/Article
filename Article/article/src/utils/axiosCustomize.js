import axios from "axios";
import Cookies from "js-cookie";

const API_BASE_URL = "https://localhost:7085";

const axiosPublic = axios.create({
  baseURL: API_BASE_URL,
  headers: { "Content-Type": "application/json" },
});

const axiosPrivate = axios.create({
  baseURL: API_BASE_URL,
  headers: { "Content-Type": "application/json" },
});

let isRefreshing = false; // Trạng thái refresh token
let failedQueue = []; // Hàng đợi cho các request đang chờ

const processQueue = (error, token = null) => {
  failedQueue.forEach((promise) => {
    if (error) {
      promise.reject(error);
    } else {
      promise.resolve(token);
    }
  });
  failedQueue = [];
};

axiosPrivate.interceptors.request.use(
  (config) => {
    const accessToken = Cookies.get("accessToken");
    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

axiosPrivate.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (
      error.response &&
      error.response.status === 401 &&
      !originalRequest._retry
    ) {
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        })
          .then((token) => {
            originalRequest.headers.Authorization = `Bearer ${token}`;
            return axiosPrivate(originalRequest);
          })
          .catch((err) => Promise.reject(err));
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const accessToken = Cookies.get("accessToken");
        const refreshToken = Cookies.get("refreshToken");
        const response = await axiosPublic.post(
          "/api/ArticleToken/RefreshToken",
          { accessToken, refreshToken }
        );

        if (response && response.accessToken && response.refreshToken) {
          const newAccessToken = response.accessToken;
          const newRefreshToken = response.refreshToken;

          // Cập nhật token trong Cookies
          Cookies.set("accessToken", newAccessToken, { expires: 1 }); 
          Cookies.set("refreshToken", newRefreshToken, { expires: 2 });

          // Cập nhật Authorization header
          originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
          processQueue(null, newAccessToken);

          return axiosPrivate(originalRequest);  
        } else {
          throw new Error("Invalid refresh token response");
        }
      } catch (err) {
        processQueue(err, null);
        Cookies.remove("accessToken");
        Cookies.remove("refreshToken");
        window.location.href = "/login";  
        return Promise.reject(err);
      } finally {
        isRefreshing = false;
      }
    }

    return Promise.reject(error);
  }
);

axiosPublic.interceptors.response.use(
  (response) => response.data,
  (error) => Promise.reject(error)
);

export { axiosPublic, axiosPrivate };
