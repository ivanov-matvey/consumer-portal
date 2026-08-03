<template>
  <header class="header">
    <div class="header__container">
      <router-link class="header__logo" to="/">Портал потребителей</router-link>
      <nav class="header__navigation">
        <router-link class="header__link" to="/">Организации</router-link>
        <router-link class="header__link" to="/claim/new">
          Подать жалобу
        </router-link>
        <template v-if="isAuthenticated">
          <span class="header__user">{{ user.fullName }}</span>
          <button class="header__logout" type="button" @click="logout">
            Выйти
          </button>
        </template>
        <template v-else>
          <router-link class="header__link" to="/login">Войти</router-link>
          <router-link class="header__link" to="/register"
            >Регистрация</router-link
          >
        </template>
      </nav>
    </div>
  </header>
</template>

<script>
export default {
  name: "AppHeader",
  computed: {
    isAuthenticated() {
      return this.$store.getters.isAuthenticated;
    },
    user() {
      return this.$store.state.user;
    },
  },
  methods: {
    async logout() {
      this.$store.dispatch("logout");
      if (this.$route.meta.requiresAuth) {
        await this.$router.push({ name: "home" });
      }
    },
  },
};
</script>

<style scoped>
.header {
  background: var(--color-header);
  color: var(--color-on-dark);
}
.header__container {
  width: min(1200px, calc(100% - 32px));
  min-height: 72px;
  margin: 0 auto;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 24px;
}
.header__logo {
  color: var(--color-on-dark);
  font-size: var(--font-size-logo);
  font-weight: var(--font-weight-bold);
  text-decoration: none;
}
.header__navigation {
  display: flex;
  align-items: center;
  gap: 20px;
}
.header__link {
  color: var(--color-on-dark-muted);
  text-decoration: none;
}
.header__link:hover,
.header__link.router-link-exact-active {
  color: var(--color-on-dark);
}
.header__user {
  color: var(--color-on-dark-muted);
  font-size: var(--font-size-caption);
}
.header__logout {
  border: 0;
  padding: 0;
  background: transparent;
  color: var(--color-on-dark-muted);
  cursor: pointer;
}
.header__logout:hover {
  color: var(--color-on-dark);
}
@media (max-width: 760px) {
  .header__container {
    padding: 16px 0;
    flex-direction: column;
    align-items: flex-start;
  }
  .header__navigation {
    flex-wrap: wrap;
    gap: 12px;
  }
}
</style>
