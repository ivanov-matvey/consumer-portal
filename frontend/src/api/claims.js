import http from "./http";

export async function createClaim(payload) {
  const response = await http.post("/api/claims", payload);
  return response.data;
}

export async function getMyClaims() {
  const response = await http.get("/api/claims/my");
  return response.data;
}

export async function getClaims() {
  const response = await http.get("/api/claims");
  return response.data;
}

export async function updateClaimStatus(id, status) {
  const response = await http.patch(`/api/claims/${id}/status`, { status });
  return response.data;
}
