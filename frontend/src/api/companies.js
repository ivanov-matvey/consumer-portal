import http from "./http";

export async function getCompanies() {
  const response = await http.get("/api/companies");
  return response.data;
}

export async function getCompany(id) {
  const response = await http.get(`/api/companies/${id}`);
  return response.data;
}
