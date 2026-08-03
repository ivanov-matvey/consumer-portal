import http from "./http";

export async function register(payload) {
  const response = await http.post("/api/auth/register", payload);
  return response.data;
}

export async function login(payload) {
  const response = await http.post("/api/auth/login", payload);
  return response.data;
}
