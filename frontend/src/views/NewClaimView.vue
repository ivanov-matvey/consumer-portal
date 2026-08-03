<template>
  <section class="claim-page">
    <h1>Подать жалобу</h1>
    <p class="claim-page__intro">Опишите проблему и выберите организацию.</p>

    <form class="claim-form" novalidate @submit.prevent="submitClaim">
      <div class="field">
        <label for="title">Заголовок</label>
        <input
          id="title"
          v-model.trim="form.title"
          type="text"
          maxlength="150"
        />
        <span class="field__hint">От 10 до 150 символов</span>
        <span v-if="errors.title" class="field__error">{{ errors.title }}</span>
      </div>

      <div class="field">
        <label for="text">Описание</label>
        <textarea id="text" v-model.trim="form.text" rows="7"></textarea>
        <span v-if="errors.text" class="field__error">{{ errors.text }}</span>
      </div>

      <div class="field">
        <label for="company">Организация</label>
        <select
          id="company"
          v-model="form.companyId"
          :disabled="loadingCompanies"
        >
          <option value="">Выберите организацию</option>
          <option
            v-for="company in companies"
            :key="company.id"
            :value="company.id"
          >
            {{ company.name }} — {{ company.inn }}
          </option>
        </select>
        <span v-if="errors.companyId" class="field__error">{{
          errors.companyId
        }}</span>
        <span v-if="errors.inn" class="field__error">{{ errors.inn }}</span>
      </div>

      <div v-if="errors.general" class="form-error" role="alert">
        {{ errors.general }}
      </div>
      <button
        class="submit-button"
        type="submit"
        :disabled="submitting || loadingCompanies"
      >
        {{ submitting ? "Отправляем…" : "Отправить жалобу" }}
      </button>
    </form>
  </section>
</template>

<script>
import { getCompanies } from "../api/companies";
import { createClaim } from "../api/claims";

export default {
  name: "NewClaimView",
  data: () => ({
    companies: [],
    loadingCompanies: false,
    submitting: false,
    form: { title: "", text: "", companyId: "" },
    errors: {},
  }),
  created() {
    this.loadCompanies();
  },
  methods: {
    async loadCompanies() {
      this.loadingCompanies = true;
      try {
        this.companies = await getCompanies();
        if (this.$route.query.companyId) {
          this.form.companyId = this.$route.query.companyId;
        }
      } catch (error) {
        this.errors = { general: "Не удалось загрузить список организаций." };
      } finally {
        this.loadingCompanies = false;
      }
    },
    async submitClaim() {
      this.errors = {};
      this.submitting = true;
      try {
        const company = this.companies.find(
          (item) => item.id === this.form.companyId
        );
        const claim = await createClaim({
          ...this.form,
          inn: company?.inn || "",
        });
        await this.$router.push(`/company/${claim.companyId}`);
      } catch (error) {
        this.errors = this.getApiErrors(error);
      } finally {
        this.submitting = false;
      }
    },
    getApiErrors(error) {
      const apiErrors = error.response?.data?.errors;
      if (!apiErrors) return { general: "Не удалось отправить жалобу." };

      const result = {};
      Object.entries(apiErrors).forEach(([field, messages]) => {
        result[field.charAt(0).toLowerCase() + field.slice(1)] = messages[0];
      });
      return result;
    },
  },
};
</script>

<style scoped>
.claim-page {
  max-width: 720px;
  margin: 0 auto;
}
.claim-page h1 {
  margin-bottom: 8px;
}
.claim-page__intro {
  margin-bottom: 28px;
  color: var(--color-text-secondary);
}
.claim-form {
  padding: 28px;
  border-radius: 12px;
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
  display: grid;
  gap: 22px;
}
.field {
  display: grid;
  gap: 7px;
}
.field label {
  font-weight: var(--font-weight-bold);
}
.field input,
.field textarea,
.field select {
  width: 100%;
  padding: 11px 12px;
  border: 1px solid var(--color-border);
  border-radius: 8px;
  background: var(--color-surface);
  color: var(--color-text);
}
.field textarea {
  resize: vertical;
}
.field__hint {
  color: var(--color-text-secondary);
  font-size: var(--font-size-caption);
}
.field__error,
.form-error {
  color: var(--color-error);
  font-size: var(--font-size-caption);
}
.form-error {
  padding: 12px;
  border: 1px solid var(--color-error-border);
  border-radius: 8px;
}
.submit-button {
  justify-self: start;
  padding: 11px 18px;
  border: 0;
  border-radius: 8px;
  background: var(--color-accent-text);
  color: var(--color-on-dark);
  cursor: pointer;
}
.submit-button:disabled {
  opacity: 0.6;
  cursor: wait;
}
</style>
