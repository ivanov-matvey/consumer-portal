import http from "./http";

export async function createClaim(payload) {
  const response = await http.post("/api/claims", payload);
  return response.data;
}

export async function getMyClaims(params) {
  const response = await http.get("/api/claims/my", { params });
  return response.data;
}

export async function getClaims(params) {
  const response = await http.get("/api/claims", { params });
  return response.data;
}

export async function updateClaimStatus(id, status) {
  const response = await http.patch(`/api/claims/${id}/status`, { status });
  return response.data;
}
