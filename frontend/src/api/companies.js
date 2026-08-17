import http from "./http";

export async function getCompanies(params) {
  const response = await http.get("/api/companies", { params });
  return response.data;
}

export async function getCompany(id, params) {
  const response = await http.get(`/api/companies/${id}`, { params });
  return response.data;
}
