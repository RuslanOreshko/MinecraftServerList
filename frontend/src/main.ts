import { createApp } from "vue";
import { createPinia } from "pinia";
import { useAuthStore } from "@/features/auth/model/useAuthStore";
import "./assets/main.css";

import App from "./App.vue";
import router from "./router";

const app = createApp(App);

app.use(createPinia());

const authStore = useAuthStore();
await authStore.init();

app.use(router);

app.mount("#app");
