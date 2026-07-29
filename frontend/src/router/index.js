import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "../views/HomeView.vue";
import CompanyView from "../views/CompanyView.vue";
import NewClaimView from "../views/NewClaimView.vue";

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
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
