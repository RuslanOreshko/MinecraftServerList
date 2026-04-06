<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { register } from "@/features/auth/api/auth.api";

const router = useRouter();

const email = ref("");
const password = ref("");
const userName = ref("");
const error = ref("");

const handleRegister = async () => {
  try {
    error.value = "";

    await register(email.value, password.value, userName.value);

    router.push("/login");
  } catch (e) {
    error.value = "Registration failed";
  }
};
</script>

<template>
  <div class="auth-page">
    <div class="card">
      <h2>Register</h2>

      <input v-model="userName" placeholder="Username" />
      <input v-model="email" placeholder="Email" />
      <input v-model="password" type="password" placeholder="Password" />

      <p v-if="error" class="error">{{ error }}</p>

      <button @click="handleRegister">Create account</button>

      <router-link to="/login">Already have account?</router-link>
    </div>
  </div>
</template>

<style scoped>
.auth-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 80vh;
}

.card {
  background: white;
  padding: 30px;
  border-radius: 16px;
  width: 320px;
  border: 1px solid #e5e7eb;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

input {
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #e5e7eb;
}

button {
  background: #f59e0b;
  border: none;
  padding: 10px;
  border-radius: 8px;
  color: white;
  cursor: pointer;
}

.error {
  color: red;
  font-size: 13px;
}
</style>
