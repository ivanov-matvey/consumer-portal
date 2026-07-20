<template>
  <header class="header">
    <div class="header__container">
      <router-link class="header__logo" to="/">
        Портал потребителей
      </router-link>

      <nav class="header__navigation">
        <router-link class="header__link" to="/"> Организации </router-link>

        <router-link class="header__link" to="/claim/new">
          Подать жалобу
        </router-link>

        <router-link class="header__link" to="/profile"> Профиль </router-link>

        <router-link class="header__link" to="/login"> Войти </router-link>
      </nav>
    </div>
  </header>
</template>

<script>
export default {
  name: "AppHeader",
};
</script>

<style scoped>
.header {
  background: #1f2937;
  color: #ffffff;
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
  color: #ffffff;
  font-size: 22px;
  font-weight: 700;
  text-decoration: none;
}

.header__nav {
  display: flex;
  align-items: center;
  gap: 20px;
}

.header__link {
  color: #d1d5db;
  text-decoration: none;
}

.header__link:hover,
.header__link::router-link-exact-active {
  color: #ffffff;
}

@media (max-width: 760px) {
  .header__container {
    padding: 16px 0;
    flex-direction: column;
    align-items: flex-start;
  }

  .header__nav {
    flex-wrap: wrap;
    gap: 12px;
  }
}
</style>
