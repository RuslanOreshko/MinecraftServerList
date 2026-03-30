import { http } from "./http";

export const getServer = async () => {
  const response = await http.get("server");
  return response.data;
};
