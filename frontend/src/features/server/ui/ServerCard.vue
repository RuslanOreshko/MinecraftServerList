<script setup lang="ts">
import type { Server } from "@/features/server/types/server.types";
import { useRouter } from "vue-router";

const props = defineProps<{ server: Server }>();
const router = useRouter();

const go = () => {
  router.push(`/server/${props.server.id}`);
};
</script>

<template>
  <div class="card" @click="go">
    <div class="top">
      <div class="icon">🎮</div>

      <span
        class="status"
        :class="props.server.status === 1 ? 'online' : 'offline'"
      />
    </div>

    <h3>{{ props.server.name }}</h3>

    <p class="ip">{{ props.server.ip }}:{{ props.server.port }}</p>

    <div class="tags">
      <span>{{ props.server.mode }}</span>
      <span>{{ props.server.version }}</span>
    </div>

    <div class="bottom">
      <span>👥 {{ props.server.onlinePlayers }}</span>
      <span>⭐ {{ props.server.averageRating }}</span>
    </div>

    <button class="learn">View</button>
  </div>
</template>

<style scoped>
.card {
  background: rgba(255, 255, 255, 0.7);
  backdrop-filter: blur(12px);

  border-radius: 20px;
  padding: 16px;

  display: flex;
  flex-direction: column;
  gap: 10px;

  cursor: pointer;
  transition: 0.25s;

  border: 1px solid rgba(0, 0, 0, 0.05);
}

.card:hover {
  transform: translateY(-5px);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.1);
}

.top {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.icon {
  width: 36px;
  height: 36px;
  border-radius: 10px;
  background: #f1f5f9;

  display: flex;
  align-items: center;
  justify-content: center;
}

.status {
  width: 10px;
  height: 10px;
  border-radius: 50%;
}

.online {
  background: #22c55e;
}

.offline {
  background: #ef4444;
}

h3 {
  margin: 0;
}

.ip {
  color: #64748b;
  font-size: 14px;
}

.tags {
  display: flex;
  gap: 6px;
}

.tags span {
  background: #e2e8f0;
  padding: 4px 8px;
  border-radius: 6px;
  font-size: 12px;
}

.bottom {
  display: flex;
  justify-content: space-between;
  font-size: 14px;
}

.learn {
  margin-top: auto;
  border: none;
  background: #f1f5f9;
  padding: 8px;
  border-radius: 10px;
  cursor: pointer;
}

.learn:hover {
  background: #e2e8f0;
}
</style>
