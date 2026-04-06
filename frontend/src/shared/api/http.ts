import axios from "axios";
import { API_BASE_URL } from "../config/api";
import { useAuthStore } from "@/features/auth/model/useAuthStore";
import { refresh, logout } from "./auth.api";
import { getMe } from "./auth.api";

export const http = axios.create({
  baseURL: API_BASE_URL,
  withCredentials: true,
});

http.interceptors.request.use((config) => {
  const authStroe = useAuthStore();

  if (authStroe.accessToken) {
    config.headers = config.headers ?? {};
    (config.headers as any).Authorization = `Bearer ${authStroe.accessToken}`;
  }

  return config;
});

let isRefreshing = false;
let fileedQueue: any[] = [];

const processQueue = (error: any, token: string | null = null) => {
  fileedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });

  fileedQueue = [];
};

http.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    const authStore = useAuthStore();

    if (
      error.response?.status === 401 &&
      !originalRequest._retry &&
      !originalRequest.url?.includes("/auth/refresh")
    ) {
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          fileedQueue.push({
            resolve: (token: string) => {
              originalRequest.headers.Authorization = `Bearer ${token}`;
              resolve(http(originalRequest));
            },
            reject: (err: any) => reject(err),
          });
        });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const response = await refresh();

        authStore.setAccessToken(response.accessToken);

        const me = await getMe();
        authStore.setUser(me);

        processQueue(null, response.accessToken);

        originalRequest.headers.Authorization = `Bearer ${response.accessToken}`;

        return http(originalRequest);
      } catch (err) {
        processQueue(err, null);

        authStore.logout();
        await logout();

        return Promise.reject(err);
      } finally {
        isRefreshing = false;
      }
    }

    return Promise.reject(error);
  },
);
