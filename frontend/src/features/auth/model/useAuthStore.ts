import { computed, ref } from "vue";
import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", () => {
  const accessToken = ref<string | null>(null);

  const isAuthenricated = computed(() => !!accessToken.value);

  const setAccessToken = (token: string | null) => {
    accessToken.value = token;
  };

  const logout = () => {
    accessToken.value = null;
  };

  return {
    accessToken,
    isAuthenricated,
    setAccessToken,
    logout,
  };
});
