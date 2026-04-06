import { http } from "./http";
import type {
  LoginRequest,
  LoginResponse,
} from "@/features/auth/types/auth.types";

export const login = async (payload: LoginRequest) => {
  const response = await http.post<LoginResponse>("/auth/login", payload);
  return response.data;
};

export const refresh = async () => {
  const response = await http.post<LoginResponse>("/auth/refresh");
  return response.data;
};

export const getMe = async () => {
  const response = await http.get("/auth/me");
  return response.data;
};

export const logout = async () => {
  await http.post("/auth/logout");
};

export const getPendingServers = async () => {
  const res = await http.get("/moderator/servers/pending");
  return res.data.items ?? res.data;
};

export const approveServer = async (id: string) => {
  await http.post(`/moderator/servers/${id}/approve`);
};

export const rejectServer = async (id: string) => {
  await http.post(`/moderator/servers/${id}/reject`);
};
