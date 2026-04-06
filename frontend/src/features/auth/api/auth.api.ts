import { http } from "@/shared/api/http";

export const login = async (email: string, password: string) => {
  const res = await http.post("/auth/login", {
    email,
    password,
  });

  return res.data;
};

export const register = async (
  email: string,
  password: string,
  userName: string,
) => {
  const res = await http.post("/auth/register", {
    email,
    password,
    userName,
  });

  return res.data;
};
