<template>
  <div id="app" class="app">
    <AppHeader />

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
};
</script>

<style>
* {
  box-sizing: border-box;
}

html,
body {
  min-height: 100%;
  margin: 0;
}

body {
  background: #f3f4f6;
  color: #1f2937;
  font-family: Arial, Helvetica, sans-serif;
}

button,
input,
textarea,
select {
  font: inherit;
}

.app {
  min-height: 100vh;

  display: grid;
  grid-template-rows: auto 1fr auto;
}

.app__main {
  padding: 40px 0;
}

.app__container {
  width: min(1200px, calc(100% - 32px));
  margin: 0 auto;
}
</style>
