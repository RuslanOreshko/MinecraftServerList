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
  const response = await http.post<LoginResponse>("/api/refresh");
  return response.data;
};

export const logout = async () => {
  await http.post("/auth/logout");
};
