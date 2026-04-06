<script setup lang="ts">
import { ref } from "vue";
import { http } from "@/shared/api/http";
import { useRouter } from "vue-router";

const router = useRouter();

const name = ref("");
const ip = ref("");
const port = ref(25565);
const country = ref("");
const mode = ref("");
const version = ref("");
const description = ref("");

const error = ref("");

const handleSubmit = async () => {
  try {
    error.value = "";

    await http.post("/server", {
      name: name.value,
      ip: ip.value,
      port: port.value,
      country: country.value,
      mode: mode.value,
      version: version.value,
      descriptions: description.value,
    });

    router.push("/");
  } catch (e) {
    error.value = "Failed to create server";
  }
};
</script>

<template>
  <div class="container">
    <h1>Add Server</h1>

    <div class="form">
      <input v-model="name" placeholder="Name" />
      <input v-model="ip" placeholder="IP" />
      <input v-model="port" type="number" placeholder="Port" />

      <input v-model="country" placeholder="Country" />
      <input v-model="mode" placeholder="Mode" />
      <input v-model="version" placeholder="Version" />

      <textarea v-model="description" placeholder="Description" />

      <p v-if="error" class="error">{{ error }}</p>

      <button @click="handleSubmit">Create</button>
    </div>
  </div>
</template>

<style scoped>
.form {
  display: flex;
  flex-direction: column;
  gap: 10px;
  max-width: 400px;
}

input,
textarea {
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
}

.error {
  color: red;
}
</style>
