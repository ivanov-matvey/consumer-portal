import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "../views/HomeView.vue";
import CompanyView from "../views/CompanyView.vue";
import NewClaimView from "../views/NewClaimView.vue";
import LoginView from "../views/LoginView.vue";
import RegisterView from "../views/RegisterView.vue";
import store from "../store";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/company/:id",
    name: "company",
    component: CompanyView,
    props: true,
  },
  {
    path: "/claim/new",
    name: "new-claim",
    component: NewClaimView,
    meta: { requiresAuth: true },
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
    meta: { requiresGuest: true },
  },
  {
    path: "/register",
    name: "register",
    component: RegisterView,
    meta: { requiresGuest: true },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  if (
    to.matched.some((route) => route.meta.requiresAuth) &&
    !store.getters.isAuthenticated
  ) {
    next({ name: "login", query: { redirect: to.fullPath } });
    return;
  }

  if (
    to.matched.some((route) => route.meta.requiresGuest) &&
    store.getters.isAuthenticated
  ) {
    next({ name: "home" });
    return;
  }

  next();
});

export default router;
