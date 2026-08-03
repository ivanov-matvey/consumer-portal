<template>
  <section class="auth-page">
    <h1>Войти</h1>
    <p class="auth-page__intro">
      Войдите, чтобы подать жалобу от своего имени.
    </p>

    <form class="auth-form" novalidate @submit.prevent="submit">
      <div class="field">
        <label for="email">Email</label>
        <input
          id="email"
          v-model.trim="form.email"
          type="email"
          autocomplete="email"
        />
        <span v-if="errors.email" class="field__error">{{ errors.email }}</span>
      </div>
      <div class="field">
        <label for="password">Пароль</label>
        <input
          id="password"
          v-model="form.password"
          type="password"
          autocomplete="current-password"
        />
        <span v-if="errors.password" class="field__error">{{
          errors.password
        }}</span>
      </div>
      <p v-if="errors.general" class="form-error" role="alert">
        {{ errors.general }}
      </p>
      <button class="submit-button" type="submit" :disabled="submitting">
        {{ submitting ? "Входим…" : "Войти" }}
      </button>
    </form>
    <p class="auth-page__alternate">
      Нет учётной записи?
      <router-link to="/register">Зарегистрироваться</router-link>
    </p>
  </section>
</template>

<script>
export default {
  name: "LoginView",
  data: () => ({
    form: { email: "", password: "" },
    errors: {},
    submitting: false,
  }),
  methods: {
    async submit() {
      this.errors = {};
      this.submitting = true;
      try {
        await this.$store.dispatch("login", this.form);
        await this.$router.replace(this.$route.query.redirect || "/");
      } catch (error) {
        this.errors = this.getErrors(error);
      } finally {
        this.submitting = false;
      }
    },
    getErrors(error) {
      const apiErrors = error.response?.data?.errors;
      if (apiErrors) {
        return Object.entries(apiErrors).reduce((result, [field, messages]) => {
          result[field.charAt(0).toLowerCase() + field.slice(1)] = messages[0];
          return result;
        }, {});
      }
      return {
        general: error.response?.data?.message || "Не удалось выполнить вход.",
      };
    },
  },
};
</script>

<style scoped>
.auth-page {
  max-width: 520px;
  margin: 0 auto;
}
.auth-page__intro {
  margin-bottom: 28px;
  color: var(--color-text-secondary);
}
.auth-form {
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
.field input {
  width: 100%;
  padding: 11px 12px;
  border: 1px solid var(--color-border);
  border-radius: 8px;
  color: var(--color-text);
}
.field__error,
.form-error {
  color: var(--color-error);
  font-size: var(--font-size-caption);
}
.form-error {
  margin: 0;
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
.auth-page__alternate {
  margin-top: 20px;
}
.auth-page__alternate a {
  color: var(--color-accent-text);
}
</style>
