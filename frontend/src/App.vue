<template>
  <div id="app" class="app">
    <AppHeader />

    <p v-if="serverError" class="app__server-error" role="alert">
      {{ serverError }}
    </p>

    <main class="app__main">
      <div class="app__container">
        <router-view />
      </div>
    </main>

    <AppFooter />
  </div>
</template>

<script>
import AppHeader from "./components/AppHeader.vue";
import AppFooter from "./components/AppFooter.vue";

export default {
  name: "App",

  components: {
    AppHeader,
    AppFooter,
  },
  data: () => ({ serverError: "" }),
  created() {
    window.addEventListener(
      "consumer-portal-server-error",
      this.showServerError
    );
  },
  beforeDestroy() {
    window.removeEventListener(
      "consumer-portal-server-error",
      this.showServerError
    );
  },
  methods: {
    showServerError(event) {
      this.serverError = event.detail;
    },
  },
};
</script>

<style>
.app {
  min-height: 100vh;

  display: grid;
  grid-template-rows: auto 1fr auto;
}

.app__main {
  padding: 40px 0;
}

.app__server-error {
  width: min(1200px, calc(100% - 32px));
  margin: 16px auto 0;
  padding: 12px 16px;
  border: 1px solid var(--color-error-border);
  border-radius: 8px;
  background: var(--color-surface);
  color: var(--color-error);
}

.app__container {
  width: min(1200px, calc(100% - 32px));
  margin: 0 auto;
}
</style>
