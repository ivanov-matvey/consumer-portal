<template>
  <section>
    <header class="page-heading">
      <h1>Мои жалобы</h1>
      <router-link class="primary-link" to="/claim/new"
        >Подать жалобу</router-link
      >
    </header>

    <p v-if="loading" class="state-card">Загружаем ваши жалобы…</p>
    <p v-else-if="error" class="state-card state-card--error" role="alert">
      {{ error }}
    </p>
    <p v-else-if="claims.length === 0" class="state-card">
      Вы ещё не подавали жалоб.
    </p>
    <div v-else class="claims-list">
      <article v-for="claim in claims" :key="claim.id" class="claim-card">
        <div class="claim-card__heading">
          <div>
            <h2>{{ claim.title }}</h2>
            <router-link :to="`/company/${claim.companyId}`">
              {{ claim.companyName }}
            </router-link>
          </div>
          <span class="status">{{ statusLabel(claim.status) }}</span>
        </div>
        <p>{{ claim.text }}</p>
        <time :datetime="claim.createdAt">{{
          formatDate(claim.createdAt)
        }}</time>
      </article>
    </div>
  </section>
</template>

<script>
import { getMyClaims } from "../api/claims";

const STATUS_LABELS = {
  1: "Новое",
  2: "В работе",
  3: "Решено",
  4: "Отклонено",
};

export default {
  name: "ProfileView",
  data: () => ({ claims: [], loading: false, error: "" }),
  created() {
    this.loadClaims();
  },
  methods: {
    async loadClaims() {
      this.loading = true;
      this.error = "";
      try {
        this.claims = await getMyClaims();
      } catch (error) {
        this.error = "Не удалось загрузить ваши жалобы.";
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
.page-heading {
  margin-bottom: 28px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 20px;
}
.page-heading h1 {
  margin: 0;
}
.primary-link {
  padding: 11px 18px;
  border-radius: 8px;
  background: var(--color-accent-text);
  color: var(--color-on-dark);
  text-decoration: none;
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
.claim-card h2 {
  margin: 0 0 8px;
}
.claim-card a {
  color: var(--color-accent-text);
}
.claim-card p {
  margin: 16px 0;
  white-space: pre-wrap;
}
.claim-card time {
  color: var(--color-text-secondary);
  font-size: var(--font-size-caption);
}
.status {
  height: fit-content;
  padding: 5px 10px;
  border-radius: 999px;
  background: var(--color-accent-surface);
  color: var(--color-accent-text);
  font-size: var(--font-size-caption);
  font-weight: var(--font-weight-bold);
  white-space: nowrap;
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
  .page-heading,
  .claim-card__heading {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
