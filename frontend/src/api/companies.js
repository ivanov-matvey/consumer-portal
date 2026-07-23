import axios from "axios";

export async function getCompanies() {
  const response = await axios.get("/api/companies");
  return response.data;
}

export async function getCompany(id) {
  const response = await axios.get(`/api/companies/${id}`);
  return response.data;
}
