<script setup lang="ts">
import { useRouter } from "vue-router";
import { useAuthStore } from "@/features/auth/model/useAuthStore";
import { logout as logoutApi } from "@/shared/api/auth.api";

const router = useRouter();
const authStore = useAuthStore();

const goHome = () => router.push("/");
const goLogin = () => router.push("/login");
const goModeration = () => router.push("/moderation");
const goAddServer = () => router.push("/add-server");

const handleLogout = async () => {
  try {
    await logoutApi();
  } catch (e) {
    console.error("Logout error:", e);
  }

  authStore.logout();
};
</script>

<template>
  <header class="header">
    <div class="container header-inner">
      <div class="logo" @click="goHome">
        <span class="logo-main">Minecraft</span>
        <span class="logo-accent">List</span>
      </div>

      <div class="nav">
        <button
          v-if="authStore.canManageServers"
          class="btn ghost"
          @click="goModeration"
        >
          Moderation
        </button>

        <button
          v-if="authStore.canManageServers"
          class="btn primary"
          @click="goAddServer"
        >
          + Add Server
        </button>

        <button
          v-if="!authStore.isAuthenricated"
          class="btn outline"
          @click="goLogin"
        >
          Login
        </button>

        <div v-else class="user">
          <div class="avatar">
            {{ authStore.user?.userName?.[0]?.toUpperCase() }}
          </div>

          <span class="username">
            {{ authStore.user?.userName }}
          </span>

          <button class="btn danger" @click="handleLogout">Logout</button>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped>
.header {
  position: sticky;
  top: 0;
  z-index: 100;

  background: rgba(255, 255, 255, 0.7);
  backdrop-filter: blur(12px);

  border-bottom: 1px solid rgba(0, 0, 0, 0.05);
}

.header-inner {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.logo {
  font-size: 20px;
  font-weight: 700;
  cursor: pointer;
}

.logo-main {
  color: #0f172a;
}

.logo-accent {
  color: #f59e0b;
}

.nav {
  display: flex;
  align-items: center;
  gap: 10px;
}

.btn {
  border: none;
  padding: 8px 14px;
  border-radius: 10px;
  cursor: pointer;
  font-weight: 500;
  transition: 0.2s;
}

.primary {
  background: linear-gradient(135deg, #f59e0b, #f97316);
  color: white;
}

.primary:hover {
  transform: translateY(-1px);
  box-shadow: 0 8px 20px rgba(249, 115, 22, 0.3);
}

.outline {
  background: transparent;
  border: 1px solid #cbd5e1;
}

.outline:hover {
  background: #f1f5f9;
}

.ghost {
  background: transparent;
  color: #475569;
}

.ghost:hover {
  background: #f1f5f9;
}

.danger {
  background: #ef4444;
  color: white;
}

.danger:hover {
  background: #dc2626;
}

.user {
  display: flex;
  align-items: center;
  gap: 10px;
}

.avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;

  background: linear-gradient(135deg, #f59e0b, #f97316);
  color: white;

  display: flex;
  align-items: center;
  justify-content: center;

  font-weight: 600;
}

.username {
  font-size: 14px;
  color: #334155;
}
</style>
