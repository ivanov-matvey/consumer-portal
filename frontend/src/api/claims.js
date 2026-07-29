import axios from "axios";

export async function createClaim(payload) {
  const response = await axios.post("/api/claims", payload);
  return response.data;
}
