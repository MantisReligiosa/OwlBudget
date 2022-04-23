import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';
import url from '../url.js';
import eventBus from '../eventBus';

Vue.use(Vuex);
Vue.config.devtools = true;

export default new Vuex.Store({
  state: {
    projectId: null,
    projectName: '',
    token: localStorage.getItem('token') || '',
    user: JSON.parse(localStorage.getItem('user')) || { name: '' },
    selectedBudgetType: 1,
    scenarios: [],
    selectedScenario: null,
  },
  mutations: {
    auth_success(state, payload) {
      state.token = payload.token;
      state.user = payload.user;
    },
    auth_error(state, error) {
      eventBus.$emit('message', { content: error, title: 'Авторизация', variant: 'danger' });
    },
    logout(state) {
      state.token = '';
      state.user = {};
      state.projectId = null;
      state.projectName = '';
      state.selectedBudgetType = 1;
      state.selectedScenario = null;
    },
    selectBudgetType(state, type) {
      state.selectedBudgetType = type;
      state.selectedScenario = state.scenarios.find((scenario) => scenario.budgetType === type);
    },
    selectScenarioById(state, id) {
      state.selectedScenario = state.scenarios.find((scenario) => scenario.id === id);
    },
    openProject_success(state, project) {
      state.projectId = project.id;
      state.projectName = project.name;
      state.selectedBudgetType = 1;
    },
    openProject_error(state, error) {
      eventBus.$emit('message', { content: error, title: 'Открытие проекта', variant: 'danger' });
    },
    newProject_success(state, project) {
      state.projectId = project.id;
      state.projectName = project.name;
      state.selectedBudgetType = 1;
    },
    newProject_error(state, error) {
      eventBus.$emit('message', { content: error, title: 'Создание проекта', variant: 'danger' });
    },
    loadScenarios_success(state, scenarios) {
      state.scenarios = scenarios;
      state.selectedScenario = scenarios.find((scenario) => scenario.budgetType === state.selectedBudgetType);
    },
    loadScenarios_error(state, error) {
      eventBus.$emit('message', { content: error, title: 'Загрузка сценариев', variant: 'danger' });
    },
  },
  actions: {
    login({ commit }, credits) {
      return new Promise((resolve, reject) => {
        const config = url.login;
        config.data = credits;
        axios(config)
          .then(
            (resp) => {
              const { token } = resp.data;
              const { user } = resp.data;
              localStorage.setItem('token', token);
              localStorage.setItem('user', JSON.stringify(user));
              axios.defaults.headers.common.Authorization = `Bearer ${token}`;
              commit('auth_success', { token, user });
              resolve(resp);
            },
            (reason) => {
              commit('auth_error', reason);
              reject(reason);
            },
          );
      });
    },
    logout({ commit }) {
      return new Promise((resolve) => {
        eventBus.$emit('logout');
        commit('logout');
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        delete axios.defaults.headers.common.Authorization;
        resolve();
      });
    },
    selectBudgetType({ commit }, type) {
      return new Promise((resolve) => {
        commit('selectBudgetType', type);
        resolve();
      });
    },
    selectScenarioById({ commit }, id) {
      return new Promise((resolve) => {
        commit('selectScenarioById', id);
        resolve();
      });
    },
    newProject({ commit }) {
      return new Promise((resolve, reject) => {
        const config = url.newProject;
        axios(config)
          .then(
            (resp) => {
              const project = resp.data;
              commit('newProject_success', project);
              resolve(resp);
            },
            (reason) => {
              commit('newProject_error', reason.error.response.data.title);
              reject(reason);
            },
          );
      });
    },
    openProject({ commit }, openProject) {
      return new Promise((resolve, reject) => {
        const config = url.openProject;
        config.params = {
          id: openProject.id,
        };
        axios(config)
          .then(
            (resp) => {
              const project = resp.data;
              commit('openProject_success', project);
              resolve(resp);
            },
            (reason) => {
              commit('openProject_error', reason.error.response.data.title);
              reject(reason);
            },
          );
      });
    },
    loadScenarios({ commit }, projectId) {
      return new Promise((resolve, reject) => {
        const config = url.getScenarios;
        config.params = {
          id: projectId,
        };
        axios(config)
          .then(
            (resp) => {
              const scenarios = resp.data;
              commit('loadScenarios_success', scenarios);
              resolve(resp);
            },
            (reason) => {
              commit('loadScenarios_error', reason.error.response.data.title);
              reject(reason);
            },
          );
      });
    },
  },
  getters: {
    isLoggedIn: (state) => !!state.token,
    user: (state) => state.user || { name: '' },
    projectId: (state) => state.projectId,
    projectName: (state) => state.projectName,
    selectedBudgetType: (state) => state.selectedBudgetType || 1,
    selectedScenario: (state) => state.selectedScenario,
    scenarios: (state) => state.scenarios.filter((scenario) => scenario.budgetType === state.selectedBudgetType),
  },
});
