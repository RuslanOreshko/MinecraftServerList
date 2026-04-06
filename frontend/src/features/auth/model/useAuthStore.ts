import { computed, ref } from "vue";
import { defineStore } from "pinia";
import { refresh, getMe } from "@/shared/api/auth.api";

interface User {
  userId: string;
  email: string;
  userName: string;
  roles: string[];
}

export const useAuthStore = defineStore("auth", () => {
  const accessToken = ref<string | null>(null);
  const user = ref<User | null>(null);

  const isAuthenricated = computed(() => !!accessToken.value);

  const roles = computed(() => user.value?.roles ?? []);

  const isAdmin = computed(() => roles.value.includes("Admin"));
  const isModerator = computed(() => roles.value.includes("Moderator"));

  const canManageServers = computed(() => isAdmin.value || isModerator.value);

  const setAccessToken = (token: string | null) => {
    accessToken.value = token;
  };

  const setUser = (newUser: User | null) => {
    user.value = newUser;
  };

  const logout = () => {
    accessToken.value = null;
  };

  const init = async () => {
    try {
      const response = await refresh();

      setAccessToken(response.accessToken);

      const me = await getMe();
      setUser(me);
    } catch {
      logout();
    }
  };

  return {
    accessToken,
    user,
    roles,
    isAuthenricated,
    isAdmin,
    isModerator,
    canManageServers,
    setAccessToken,
    setUser,
    logout,
    init,
  };
});
