import http from "./http";

export async function createClaim(payload) {
  const response = await http.post("/api/claims", payload);
  return response.data;
}
