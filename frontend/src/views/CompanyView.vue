<template>
  <section>
    <router-link class="back-link" to="/">← К каталогу</router-link>

    <p v-if="loading" class="state-card">Загружаем организацию…</p>
    <div v-else-if="error" class="state-card state-card--error" role="alert">
      {{ error }}
    </div>
    <template v-else-if="company">
      <header class="company-heading">
        <div>
          <span class="category">{{ categoryLabel }}</span>
          <h1>{{ company.name }}</h1>
          <p>ИНН: {{ company.inn }}</p>
        </div>
        <router-link class="primary-link" :to="claimLink"
          >Подать жалобу</router-link
        >
      </header>

      <h2 class="claims-title">Жалобы на организацию</h2>
      <p v-if="company.claims.length === 0" class="state-card">
        На эту организацию пока нет жалоб.
      </p>
      <div v-else class="claims-list">
        <article
          v-for="claim in company.claims"
          :key="claim.id"
          class="claim-card"
        >
          <div class="claim-card__heading">
            <h3>{{ claim.title }}</h3>
            <span class="status">{{ statusLabel(claim.status) }}</span>
          </div>
          <p>{{ claim.text }}</p>
          <time :datetime="claim.createdAt">{{
            formatDate(claim.createdAt)
          }}</time>
        </article>
      </div>
    </template>
  </section>
</template>

<script>
import { getCompany } from "../api/companies";

const CATEGORY_LABELS = { 1: "ЖКХ", 2: "Ритейл", 3: "Связь" };
const STATUS_LABELS = {
  1: "Новое",
  2: "В работе",
  3: "Решено",
  4: "Отклонено",
};

export default {
  name: "CompanyView",
  props: { id: { type: String, required: true } },
  data: () => ({ company: null, loading: false, error: "" }),
  computed: {
    categoryLabel() {
      return CATEGORY_LABELS[this.company.category] || "Неизвестная категория";
    },
    claimLink() {
      return { name: "new-claim", query: { companyId: this.company.id } };
    },
  },
  created() {
    this.loadCompany();
  },
  methods: {
    async loadCompany() {
      this.loading = true;
      try {
        this.company = await getCompany(this.id);
      } catch (error) {
        this.error =
          error.response?.status === 404
            ? "Организация не найдена."
            : "Не удалось загрузить организацию.";
      } finally {
        this.loading = false;
      }
    },
    statusLabel(status) {
      return STATUS_LABELS[status] || "Неизвестный статус";
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
.back-link {
  display: inline-block;
  margin-bottom: 24px;
  color: var(--color-accent-text);
  text-decoration: none;
}
.company-heading {
  padding: 28px;
  border-radius: 12px;
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 24px;
}
.company-heading h1 {
  margin: 14px 0 8px;
}
.company-heading p {
  margin: 0;
  color: var(--color-text-secondary);
}
.category,
.status {
  display: inline-block;
  padding: 5px 10px;
  border-radius: 999px;
  background: var(--color-accent-surface);
  color: var(--color-accent-text);
  font-size: var(--font-size-caption);
  font-weight: var(--font-weight-bold);
}
.primary-link {
  padding: 11px 18px;
  border-radius: 8px;
  background: var(--color-accent-text);
  color: var(--color-on-dark);
  text-decoration: none;
  white-space: nowrap;
}
.claims-title {
  margin: 36px 0 18px;
}
.claims-list {
  display: grid;
  gap: 16px;
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
.claim-card h3 {
  margin: 0 0 12px;
}
.claim-card p {
  white-space: pre-wrap;
}
.claim-card time {
  color: var(--color-text-secondary);
  font-size: var(--font-size-caption);
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
  .company-heading {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
