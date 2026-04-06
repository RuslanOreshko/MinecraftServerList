<script setup lang="ts">
import { ref, onMounted } from "vue";
import {
  getPendingServers,
  approveServer,
  rejectServer,
} from "@/shared/api/auth.api";

const servers = ref<any[]>([]);
const isLoading = ref(false);

const load = async () => {
  isLoading.value = true;

  try {
    const data = await getPendingServers();
    servers.value = data ?? [];
  } catch (e) {
    console.error(e);
    servers.value = [];
  } finally {
    isLoading.value = false;
  }
};

const handleApprove = async (id: string) => {
  await approveServer(id);
  servers.value = servers.value.filter((s) => s.id !== id);
};

const handleReject = async (id: string) => {
  await rejectServer(id);
  servers.value = servers.value.filter((s) => s.id !== id);
};

onMounted(load);
</script>

<template>
  <div class="container">
    <h1 class="title">Moderation</h1>

    <div v-if="isLoading">Loading...</div>

    <div v-else-if="servers.length === 0">No pending servers</div>

    <div v-else class="list">
      <div v-for="s in servers" :key="s.id" class="card">
        <div class="info">
          <h3>{{ s.name }}</h3>
          <p>{{ s.ip }}:{{ s.port }}</p>
          <small>{{ s.mode }} | {{ s.version }}</small>
        </div>

        <div class="actions">
          <button class="approve" @click="handleApprove(s.id)">Approve</button>

          <button class="reject" @click="handleReject(s.id)">Reject</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.title {
  font-size: 28px;
  font-weight: 700;
  margin-bottom: 20px;
}

.list {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.card {
  background: white;
  padding: 18px;
  border-radius: 14px;
  border: 1px solid #e5e7eb;

  display: flex;
  justify-content: space-between;
  align-items: center;

  transition: 0.2s;
}

.card:hover {
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.05);
}

.info h3 {
  margin: 0;
}

.info p {
  margin: 4px 0;
  color: #64748b;
}

.actions {
  display: flex;
  gap: 10px;
}

.approve {
  background: #22c55e;
  color: white;
  border: none;
  padding: 8px 14px;
  border-radius: 8px;
  cursor: pointer;
}

.reject {
  background: #ef4444;
  color: white;
  border: none;
  padding: 8px 14px;
  border-radius: 8px;
  cursor: pointer;
}
</style>
