import axios from "axios";

const http = axios.create();

http.interceptors.request.use((config) => {
  const token = localStorage.getItem("consumerPortal.accessToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

http.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error.response?.status;

    if (status === 401) {
      localStorage.removeItem("consumerPortal.accessToken");
      localStorage.removeItem("consumerPortal.user");

      if (window.location.pathname !== "/login") {
        window.location.assign("/login");
      }
    }

    if (status === 500) {
      window.dispatchEvent(
        new CustomEvent("consumer-portal-server-error", {
          detail: "Server error. Please try again later.",
        })
      );
    }

    return Promise.reject(error);
  }
);

export default http;
