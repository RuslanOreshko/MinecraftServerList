<template>
  <div>
    <h1>Minecraft server-list</h1>

    <p>Authenticated: {{ authStore.isAuthenricated }}</p>

    <button @click="setFakeToken">Set fake token</button>
    <button @click="clearToken">Logout</button>

    <button @click="loadServer">Load server</button>

    <pre>{{ servers }}</pre>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { getServer } from "@/shared/api/server.api";
import { useAuthStore } from "@/features/auth/model/useAuthStore";

const servers = ref(null);
const authStore = useAuthStore();

const loadServer = async () => {
  try {
    const data = await getServer();
    servers.value = data;
  } catch (error) {
    console.error("Failed to load servers: ", error);
  }
};

const setFakeToken = () => {
  authStore.setAccessToken("fake-jwt-token");
};

const clearToken = () => {
  authStore.logout();
};
</script>
