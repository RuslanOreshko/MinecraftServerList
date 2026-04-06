<script setup lang="ts">
import { reactive } from "vue";

const emit = defineEmits<{
  (e: "search", value: any): void;
}>();

const filter = reactive({
  country: "",
  mode: "",
  version: "",
  minRating: "",
  sortBy: "",
});

const apply = () => {
  emit("search", {
    country: filter.country || undefined,
    mode: filter.mode || undefined,
    version: filter.version || undefined,
    minRating: filter.minRating ? Number(filter.minRating) : undefined,
    sortBy: filter.sortBy || undefined,
  });
};
</script>

<template>
  <div class="search">
    <input v-model="filter.country" placeholder="Country" />
    <input v-model="filter.mode" placeholder="Mode" />
    <input v-model="filter.version" placeholder="Version" />

    <select v-model="filter.minRating">
      <option value="">Rating</option>
      <option value="1">1+</option>
      <option value="2">2+</option>
      <option value="3">3+</option>
      <option value="4">4+</option>
      <option value="5">5</option>
    </select>

    <select v-model="filter.sortBy">
      <option value="">Sort</option>
      <option value="rating">By rating</option>
      <option value="players">By players</option>
      <option value="new">Newest</option>
    </select>

    <button @click="apply">Apply</button>
  </div>
</template>

<style scoped>
.search {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
  margin-bottom: 20px;
}

input,
select {
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #e5e7eb;
}

button {
  background: #f59e0b;
  border: none;
  padding: 10px 16px;
  border-radius: 8px;
  color: white;
  cursor: pointer;
}
</style>
