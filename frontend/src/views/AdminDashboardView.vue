<template>
  <section>
    <header class="page-heading">
      <div>
        <h1>Кабинет модератора</h1>
        <p>Проверяйте жалобы и обновляйте их статус.</p>
      </div>
    </header>

    <form class="filters" @submit.prevent="applyFilters">
      <input v-model.trim="search" type="search" placeholder="Поиск жалоб" />
      <select v-model="status">
        <option value="">Все статусы</option>
        <option v-for="item in statuses" :key="item.value" :value="item.value">
          {{ item.label }}
        </option>
      </select>
      <button type="submit">Применить</button>
    </form>

    <p v-if="loading" class="state-card">Загружаем жалобы…</p>
    <p v-else-if="error" class="state-card state-card--error" role="alert">
      {{ error }}
    </p>
    <p v-else-if="claims.length === 0" class="state-card">Жалоб пока нет.</p>
    <div v-else class="claims-list">
      <article v-for="claim in claims" :key="claim.id" class="claim-card">
        <div class="claim-card__heading">
          <div>
            <h2>{{ claim.title }}</h2>
            <p class="claim-card__meta">
              {{ claim.companyName }} · {{ claim.userFullName }} ({{
                claim.userEmail
              }})
            </p>
          </div>
          <time :datetime="claim.createdAt">{{
            formatDate(claim.createdAt)
          }}</time>
        </div>
        <p class="claim-card__text">{{ claim.text }}</p>
        <div class="status-control">
          <label :for="`status-${claim.id}`">Статус</label>
          <select
            :id="`status-${claim.id}`"
            v-model.number="claim.status"
            :disabled="savingId === claim.id"
          >
            <option
              v-for="status in statuses"
              :key="status.value"
              :value="status.value"
            >
              {{ status.label }}
            </option>
          </select>
          <button
            type="button"
            :disabled="savingId === claim.id"
            @click="saveStatus(claim)"
          >
            {{ savingId === claim.id ? "Сохраняем…" : "Сохранить" }}
          </button>
        </div>
      </article>
    </div>
    <PaginationControls
      v-if="!loading && !error && pagination.totalPages > 1"
      :page="pagination.page"
      :total-pages="pagination.totalPages"
      @change="loadClaims"
    />
  </section>
</template>

<script>
import { getClaims, updateClaimStatus } from "../api/claims";
import PaginationControls from "../components/PaginationControls.vue";

export default {
  name: "AdminDashboardView",
  components: { PaginationControls },
  data: () => ({
    claims: [],
    loading: false,
    error: "",
    savingId: null,
    search: "",
    status: "",
    pagination: { page: 1, pageSize: 3, totalCount: 0, totalPages: 1 },
    statuses: [
      { value: 1, label: "Новое" },
      { value: 2, label: "В работе" },
      { value: 3, label: "Решено" },
      { value: 4, label: "Отклонено" },
    ],
  }),
  created() {
    this.loadClaims();
  },
  methods: {
    async loadClaims(page = this.pagination.page) {
      this.loading = true;
      this.error = "";
      try {
        const result = await getClaims({
          page,
          pageSize: this.pagination.pageSize,
          search: this.search || undefined,
          status: this.status || undefined,
        });
        this.claims = result.items;
        this.pagination = result;
      } catch (error) {
        this.error = "Не удалось загрузить жалобы.";
      } finally {
        this.loading = false;
      }
    },
    applyFilters() {
      this.loadClaims(1);
    },
    async saveStatus(claim) {
      this.savingId = claim.id;
      this.error = "";
      try {
        const updated = await updateClaimStatus(claim.id, claim.status);
        claim.status = updated.status;
      } catch (error) {
        this.error = "Не удалось обновить статус жалобы.";
      } finally {
        this.savingId = null;
      }
    },
    formatDate(value) {
      return new Intl.DateTimeFormat("ru-RU", {
        dateStyle: "medium",
        timeStyle: "short",
      }).format(new Date(value));
    },
  },
};
</script>

<style scoped>
.page-heading {
  margin-bottom: 28px;
}
.page-heading h1 {
  margin-bottom: 8px;
}
.page-heading p,
.claim-card__meta,
.claim-card time {
  color: var(--color-text-secondary);
}
.claims-list {
  display: grid;
  gap: 16px;
}
.filters {
  display: flex;
  gap: 10px;
  margin: 0 0 20px;
}
.filters input,
.filters select,
.filters button {
  padding: 8px 10px;
  border-radius: 8px;
  font: inherit;
}
.filters input,
.filters select {
  border: 1px solid var(--color-border);
  background: var(--color-surface);
}
.filters input {
  flex: 1;
}
.filters button {
  border: 0;
  background: var(--color-accent-text);
  color: var(--color-on-dark);
  cursor: pointer;
}
.claim-card,
.state-card {
  padding: 24px;
  border-radius: 12px;
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
}
.claim-card__heading {
  display: flex;
  justify-content: space-between;
  gap: 20px;
}
.claim-card h2,
.claim-card__meta {
  margin: 0;
}
.claim-card__text {
  margin: 18px 0;
  white-space: pre-wrap;
}
.claim-card time {
  font-size: var(--font-size-caption);
  white-space: nowrap;
}
.status-control {
  display: flex;
  align-items: center;
  gap: 10px;
}
.status-control label {
  font-weight: var(--font-weight-bold);
}
.status-control select,
.status-control button {
  padding: 8px 10px;
  border-radius: 8px;
  font: inherit;
}
.status-control select {
  border: 1px solid var(--color-border);
  background: var(--color-surface);
}
.status-control button {
  border: 0;
  background: var(--color-accent-text);
  color: var(--color-on-dark);
  cursor: pointer;
}
.status-control button:disabled,
.status-control select:disabled {
  cursor: wait;
  opacity: 0.6;
}
.state-card {
  color: var(--color-text-secondary);
  text-align: center;
}
.state-card--error {
  border: 1px solid var(--color-error-border);
  color: var(--color-error);
}
@media (max-width: 640px) {
  .claim-card__heading,
  .status-control,
  .filters {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
