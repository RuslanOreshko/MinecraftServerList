<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import {
  rateServer,
  createReview,
  getReviews,
} from "@/features/server/api/server.api";

const route = useRoute();
const id = route.params.id as string;

const reviews = ref<any[]>([]);
const reviewText = ref("");
const rating = ref(0);

const loadReviews = async () => {
  reviews.value = await getReviews(id);
};

const submitReview = async () => {
  if (!reviewText.value) return;

  await createReview(id, reviewText.value);
  reviewText.value = "";

  await loadReviews();
};

const submitRating = async (stars: number) => {
  await rateServer(id, stars);
  rating.value = stars;
};

onMounted(() => {
  loadReviews();
});
</script>

<template>
  <div class="container">
    <h1>Server</h1>

    <div class="rating">
      <span
        v-for="n in 5"
        :key="n"
        @click="submitRating(n)"
        :class="{ active: n <= rating }"
      >
        ⭐
      </span>
    </div>

    <div class="review-form">
      <textarea v-model="reviewText" placeholder="Write review..." />
      <button @click="submitReview">Send</button>
    </div>

    <div class="reviews">
      <div v-for="r in reviews" :key="r.reviewId" class="review">
        <p>{{ r.text }}</p>
        <small>{{ r.createdAt }}</small>
      </div>
    </div>
  </div>
</template>

<style scoped>
.rating {
  font-size: 24px;
  margin-bottom: 20px;
}

.rating span {
  cursor: pointer;
  opacity: 0.3;
}

.rating .active {
  opacity: 1;
}

.review-form {
  margin-bottom: 20px;
}

textarea {
  width: 100%;
  height: 80px;
  margin-bottom: 10px;
}

.review {
  padding: 10px;
  border-bottom: 1px solid #e5e7eb;
}
</style>
