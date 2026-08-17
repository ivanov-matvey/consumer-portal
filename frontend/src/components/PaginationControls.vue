<template>
  <nav class="pagination" aria-label="Pagination">
    <button
      type="button"
      :disabled="page === 1"
      @click="$emit('change', page - 1)"
    >
      Назад
    </button>
    <span>Страница {{ page }} из {{ totalPages }}</span>
    <button
      type="button"
      :disabled="page === totalPages"
      @click="$emit('change', page + 1)"
    >
      Вперёд
    </button>
  </nav>
</template>

<script>
export default {
  name: "PaginationControls",
  props: {
    page: { type: Number, required: true },
    totalPages: { type: Number, required: true },
  },
};
</script>

<style scoped>
.pagination {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
  margin-top: 24px;
}
.pagination button {
  padding: 8px 12px;
  border: 1px solid var(--color-border);
  border-radius: 8px;
  background: var(--color-surface);
  cursor: pointer;
}
.pagination button:disabled {
  cursor: not-allowed;
  opacity: 0.5;
}
</style>
