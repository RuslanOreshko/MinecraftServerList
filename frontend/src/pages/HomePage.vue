<template>
  <div>
    <h1>Minecraft server-list</h1>
    <button @click="loadServer">Load server</button>

    <pre>{{ servers }}</pre>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { getServer } from "@/shared/api/server.api";

const servers = ref(null);

const loadServer = async () => {
  try {
    const data = await getServer();
    servers.value = data;
  } catch (error) {
    console.error("Failed to load servers: ", error);
  }
};
</script>
