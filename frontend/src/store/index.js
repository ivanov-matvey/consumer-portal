import Vue from "vue";
import Vuex from "vuex";
import { login, register } from "../api/auth";

Vue.use(Vuex);

const accessTokenKey = "consumerPortal.accessToken";
const userKey = "consumerPortal.user";

function savedUser() {
  try {
    return JSON.parse(localStorage.getItem(userKey));
  } catch (error) {
    return null;
  }
}

export default new Vuex.Store({
  state: {
    accessToken: localStorage.getItem(accessTokenKey),
    user: savedUser(),
  },
  getters: {
    isAuthenticated: (state) => Boolean(state.accessToken && state.user),
    userRole: (state) => state.user?.role || null,
  },
  mutations: {
    setSession(state, session) {
      state.accessToken = session.accessToken;
      state.user = session.user;
      localStorage.setItem(accessTokenKey, session.accessToken);
      localStorage.setItem(userKey, JSON.stringify(session.user));
    },
    clearSession(state) {
      state.accessToken = null;
      state.user = null;
      localStorage.removeItem(accessTokenKey);
      localStorage.removeItem(userKey);
    },
  },
  actions: {
    async register({ commit }, payload) {
      const session = await register(payload);
      commit("setSession", session);
      return session;
    },
    async login({ commit }, payload) {
      const session = await login(payload);
      commit("setSession", session);
      return session;
    },
    logout({ commit }) {
      commit("clearSession");
    },
  },
});
