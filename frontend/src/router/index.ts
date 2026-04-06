import { createRouter, createWebHistory } from "vue-router";
import { useAuthStore } from "@/features/auth/model/useAuthStore";

const routes = [
  {
    path: "/",
    component: () => import("@/pages/HomePage.vue"),
  },
  {
    path: "/login",
    component: () => import("@/pages/LoginPage.vue"),
  },
  {
    path: "/register",
    component: () => import("@/pages/RegisterPage.vue"),
  },
  {
    path: "/server/:id",
    component: () => import("@/pages/ServerPage.vue"),
  },
  {
    path: "/add-server",
    component: () => import("@/pages/AddServerPage.vue"),
    meta: { requiresManageServers: true },
  },
  {
    path: "/moderation",
    component: () => import("@/pages/ModerationPage.vue"),
    meta: { requiresManageServers: true },
  },
  {
    path: "/server/:id",
    component: () => import("@/pages/ServerDetailPage.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to) => {
  const authStore = useAuthStore();

  if (to.meta.requiresManageServers && !authStore.canManageServers) {
    return "/";
  }
});

export default router;
