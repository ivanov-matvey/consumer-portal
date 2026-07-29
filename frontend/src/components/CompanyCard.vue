<template>
  <router-link class="company-card" :to="`/company/${company.id}`">
    <article>
      <span class="company-card__category">{{ categoryLabel }}</span>
      <h2 class="company-card__name">{{ company.name }}</h2>
      <p class="company-card__inn">ИНН: {{ company.inn }}</p>
    </article>
  </router-link>
</template>

<script>
const CATEGORY_LABELS = { 1: "ЖКХ", 2: "Ритейл", 3: "Связь" };

export default {
  name: "CompanyCard",
  props: { company: { type: Object, required: true } },
  computed: {
    categoryLabel() {
      return CATEGORY_LABELS[this.company.category] || "Неизвестная категория";
    },
  },
};
</script>

<style scoped>
.company-card {
  min-height: 180px;
  padding: 24px;
  border: 1px solid var(--color-border);
  border-radius: 12px;
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
  color: inherit;
  text-decoration: none;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
.company-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 24px rgba(31, 41, 55, 0.12);
}
.company-card__category {
  display: inline-block;
  padding: 5px 10px;
  border-radius: 999px;
  background: var(--color-accent-surface);
  color: var(--color-accent-text);
  font-size: var(--font-size-caption);
  font-weight: var(--font-weight-bold);
}
.company-card__name {
  margin: 18px 0 12px;
  font-size: var(--font-size-card-title);
}
.company-card__inn {
  margin: 0;
  color: var(--color-text-secondary);
}
</style>
