import axios from "axios";

const http = axios.create();

http.interceptors.request.use((config) => {
  const token = localStorage.getItem("consumerPortal.accessToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default http;
