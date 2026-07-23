<template>
  <section>
    <div class="catalog__heading">
      <div>
        <h1>Каталог организаций</h1>
        <p>Организации, на которые можно подать жалобу.</p>
      </div>
      <span v-if="!loading && !error" class="catalog__count">
        Организаций: {{ companies.length }}
      </span>
    </div>
    <p v-if="loading" class="catalog__message">Загружаем организации…</p>
    <div v-else-if="error" class="catalog__error" role="alert">
      <p>{{ error }}</p>
      <button type="button" @click="loadCompanies">Повторить</button>
    </div>
    <p v-else-if="companies.length === 0" class="catalog__message">
      Организации пока не добавлены.
    </p>
    <div v-else class="catalog__grid">
      <CompanyCard
        v-for="company in companies"
        :key="company.id"
        :company="company"
      />
    </div>
  </section>
</template>

<script>
import { getCompanies } from "../api/companies";
import CompanyCard from "../components/CompanyCard.vue";

export default {
  name: "HomeView",
  components: { CompanyCard },
  data: () => ({ companies: [], loading: false, error: "" }),
  created() {
    this.loadCompanies();
  },
  methods: {
    async loadCompanies() {
      this.loading = true;
      this.error = "";
      try {
        this.companies = await getCompanies();
      } catch (error) {
        this.error = "Не удалось загрузить каталог.";
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>

<style scoped>
.catalog__heading {
  margin-bottom: 28px;
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  gap: 24px;
}
h1 {
  margin: 0 0 8px;
}
.catalog__heading p,
.catalog__message {
  margin: 0;
  color: var(--color-text-secondary);
}
.catalog__count {
  white-space: nowrap;
  color: var(--color-text-tertiary);
}
.catalog__grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(260px, 1fr));
  gap: 20px;
}
.catalog__message,
.catalog__error {
  padding: 32px;
  border-radius: 12px;
  background: var(--color-surface);
  text-align: center;
}
.catalog__error {
  border: 1px solid var(--color-error-border);
  color: var(--color-error);
}
.catalog__error button {
  padding: 9px 16px;
  border: 0;
  border-radius: 8px;
  background: var(--color-error);
  color: var(--color-on-dark);
  cursor: pointer;
}
@media (max-width: 640px) {
  .catalog__heading {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
