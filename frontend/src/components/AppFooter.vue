<template>
  <footer class="footer">
    <div class="footer__container">© {{ currentYear }} Портал потребителей</div>
  </footer>
</template>

<script>
export default {
  name: "AppFooter",
  computed: {
    currentYear() {
      return new Date().getFullYear();
    },
  },
};
</script>

<style scoped>
.footer {
  background: var(--color-footer);
  color: var(--color-on-dark-muted);
}
.footer__container {
  width: min(1200px, calc(100% - 32px));
  min-height: 64px;
  margin: 0 auto;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
