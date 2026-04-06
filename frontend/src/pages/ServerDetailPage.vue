<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import {
  getReviews,
  createReview,
  rateServer,
  hideReview,
} from "@/features/server/api/server.api";
import { useServerStore } from "@/features/server/model/useServerStore";
import { useAuthStore } from "@/features/auth/model/useAuthStore";

const route = useRoute();
const serverStore = useServerStore();
const authStore = useAuthStore();

const server = ref<any>(null);
const reviews = ref<any[]>([]);
const newReview = ref("");

const id = route.params.id as string;

onMounted(async () => {
  server.value = serverStore.servers.find((s) => s.id === id);
  reviews.value = await getReviews(id);
});

const submitReview = async () => {
  if (!newReview.value.trim()) return;

  await createReview(id, newReview.value);
  reviews.value = await getReviews(id);
  newReview.value = "";
};

const submitRating = async (stars: number) => {
  await rateServer(id, stars);
};

const handleHideReview = async (reviewId: string) => {
  await hideReview(reviewId);
  reviews.value = await getReviews(id);
};
</script>

<template>
  <div class="container" v-if="server">
    <div class="card header">
      <div>
        <h1>{{ server.name }}</h1>
        <p>{{ server.ip }}:{{ server.port }}</p>
      </div>

      <span
        class="status"
        :class="server.status === 1 ? 'online' : 'offline'"
      />
    </div>

    <div class="card">
      <div class="tags">
        <span>{{ server.mode }}</span>
        <span>{{ server.version }}</span>
      </div>

      <div class="stats">
        <span>👥 {{ server.onlinePlayers }}</span>
        <span>⭐ {{ server.averageRating }}</span>
      </div>
    </div>

    <div class="card">
      <h3>Rate server</h3>

      <div class="stars">
        <span v-for="i in 5" :key="i" @click="submitRating(i)"> ⭐ </span>
      </div>
    </div>

    <div class="card">
      <h3>Reviews</h3>

      <div class="review-input">
        <input v-model="newReview" placeholder="Write review..." />
        <button class="btn primary" @click="submitReview">Send</button>
      </div>

      <div v-for="r in reviews" :key="r.reviewId" class="review">
        <div class="review-top">
          <p>{{ r.text }}</p>

          <button
            v-if="authStore.canManageServers"
            class="hide-btn"
            @click="handleHideReview(r.reviewId)"
          >
            Hide
          </button>
        </div>

        <small>{{ r.createdAt }}</small>
      </div>
    </div>
  </div>
</template>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.status {
  width: 12px;
  height: 12px;
  border-radius: 50%;
}

.online {
  background: #22c55e;
}

.offline {
  background: #ef4444;
}

.tags {
  display: flex;
  gap: 10px;
}

.tags span {
  background: #e2e8f0;
  padding: 6px 10px;
  border-radius: 8px;
}

.stats {
  margin-top: 10px;
  display: flex;
  gap: 20px;
}

.stars span {
  cursor: pointer;
  font-size: 22px;
}

.review-input {
  display: flex;
  gap: 10px;
  margin-bottom: 15px;
}

.review-input input {
  flex: 1;
  padding: 8px;
  border-radius: 8px;
  border: 1px solid #e2e8f0;
}

.review {
  padding: 10px 0;
  border-bottom: 1px solid #e2e8f0;
}

.review-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.btn {
  padding: 8px 12px;
  border-radius: 8px;
  cursor: pointer;
  border: none;
}

.primary {
  background: #f59e0b;
  color: white;
}

.hide-btn {
  background: transparent;
  border: none;
  color: #ef4444;
  cursor: pointer;
}

.hide-btn:hover {
  text-decoration: underline;
}
</style>
