<template>
  <div>
    <h1>Login</h1>

    <form @submit.prevent="handleLogin">
      <div>
        <input v-model="email" type="email" placeholder="Email..." />
      </div>

      <div>
        <input v-model="password" type="password" placeholder="Password..." />
      </div>

      <button type="submit">Login</button>
    </form>

    <p v-if="errorMessage">{{ errorMessage }}</p>
    <p>Is authenticated: {{ authStore.isAuthenricated }}</p>
    <p>Token: {{ authStore.accessToken }}</p>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { login } from "@/shared/api/auth.api";
import { useAuthStore } from "@/features/auth/model/useAuthStore";

const authStore = useAuthStore();

const email = ref("");
const password = ref("");
const errorMessage = ref("");

const handleLogin = async () => {
  errorMessage.value = "";

  try {
    const response = await login({
      email: email.value,
      password: password.value,
    });

    authStore.setAccessToken(response.accessToken);
  } catch (error) {
    console.error("Login failed:", error);
    errorMessage.value = "Login failed";
  }
};
</script>
