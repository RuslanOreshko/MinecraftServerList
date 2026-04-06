export interface Server {
  id: string;
  name: string;
  ip: string;
  port: number;
  country: string;
  mode: string;
  version: string;
  status: number;
  onlinePlayers: number;
  averageRating: number;
  ratingsCount: number;
}
