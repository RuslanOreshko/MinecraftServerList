<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useServerStore } from "@/features/server/model/useServerStore";
import ServerCard from "@/features/server/ui/ServerCard.vue";

const serverStore = useServerStore();

const filters = ref({
  country: "",
  mode: "",
  version: "",
  minRating: "",
  sortBy: "",
});

onMounted(() => {
  serverStore.loadServers();
});

const applyFilters = () => {
  serverStore.loadServers({
    country: filters.value.country,
    mode: filters.value.mode,
    version: filters.value.version,
    minRating: filters.value.minRating
      ? Number(filters.value.minRating)
      : undefined,
    sortBy: filters.value.sortBy,
  });
};
</script>

<template>
  <div class="container">
    <h1 class="title">Servers</h1>

    <div class="filters">
      <input v-model="filters.country" placeholder="Country" />
      <input v-model="filters.mode" placeholder="Mode" />
      <input v-model="filters.version" placeholder="Version" />

      <select v-model="filters.minRating">
        <option value="">Rating</option>
        <option value="1">1+</option>
        <option value="2">2+</option>
        <option value="3">3+</option>
        <option value="4">4+</option>
        <option value="5">5</option>
      </select>

      <select v-model="filters.sortBy">
        <option value="">Sort</option>
        <option value="rating">Rating</option>
        <option value="online">Online</option>
      </select>

      <button class="btn primary" @click="applyFilters">Apply</button>
    </div>

    <div class="grid">
      <ServerCard
        v-for="server in serverStore.servers"
        :key="server.id"
        :server="server"
      />
    </div>
  </div>
</template>

<style scoped>
.title {
  font-size: 28px;
  font-weight: 700;
  margin-bottom: 20px;
}

.filters {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-bottom: 25px;
}

.filters input,
.filters select {
  padding: 8px 10px;
  border-radius: 10px;
  border: 1px solid #e2e8f0;
  background: white;
}

.filters input:focus,
.filters select:focus {
  outline: none;
  border-color: #f59e0b;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
  gap: 20px;
}

.btn {
  border: none;
  padding: 10px 16px;
  border-radius: 12px;
  cursor: pointer;
  font-weight: 500;
  transition: 0.2s ease;
}

.primary {
  background: linear-gradient(135deg, #f59e0b, #f97316);
  color: white;

  box-shadow: 0 6px 16px rgba(249, 115, 22, 0.3);
}

.primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 10px 25px rgba(249, 115, 22, 0.4);
}

.primary:active {
  transform: scale(0.96);
}
</style>
