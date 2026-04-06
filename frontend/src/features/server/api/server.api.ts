import { http } from "@/shared/api/http";
import type { Server } from "../types/server.types";

export interface ServerFilter {
  country?: string;
  mode?: string;
  version?: string;
  minRating?: number;
  sortBy?: string;
  page?: number;
  pageSize?: number;
}

export const getServers = async (filter?: ServerFilter): Promise<Server[]> => {
  const response = await http.get("/server", {
    params: {
      Country: filter?.country,
      Mode: filter?.mode,
      Version: filter?.version,
      MinRating: filter?.minRating,
      SortBy: filter?.sortBy,
      Page: filter?.page ?? 1,
      PageSize: filter?.pageSize ?? 20,
    },
  });

  const data = response.data.items ?? response.data;
  return Array.isArray(data) ? data : [];
};

export const rateServer = async (id: string, stars: number) => {
  await http.post(`/server/${id}/rating`, {
    stars,
  });
};

export const createReview = async (id: string, text: string) => {
  await http.post(`/server/${id}/reviews`, {
    text,
  });
};

export const getReviews = async (id: string) => {
  const res = await http.get(`/server/${id}/reviews`);

  return res.data.items ?? [];
};

export const hideReview = async (id: string) => {
  await http.patch(`/moderator/reviews/${id}/hide`);
};
