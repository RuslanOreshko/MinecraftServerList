import { defineStore } from "pinia";
import { ref } from "vue";
import { getServers, type ServerFilter } from "../api/server.api";
import type { Server } from "../types/server.types";

export const useServerStore = defineStore("server", () => {
  const servers = ref<Server[]>([]);
  const isLoading = ref(false);

  const filter = ref<ServerFilter>({});

  const loadServers = async (newFilter?: ServerFilter) => {
    isLoading.value = true;

    try {
      filter.value = { ...filter.value, ...newFilter };

      const data = await getServers(filter.value);
      servers.value = data.filter((x) => x && x.id);
    } finally {
      isLoading.value = false;
    }
  };

  return {
    servers,
    isLoading,
    filter,
    loadServers,
  };
});
