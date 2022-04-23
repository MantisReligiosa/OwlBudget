import Vue from 'vue';
import IdleVue from 'idle-vue';
import axios from 'axios';
import VueRouter from 'vue-router';
import {CollapsePlugin, ModalPlugin} from 'bootstrap-vue';
import App from './App.vue';
import './app.scss';
import router from './router';
import store from './store';
import eventBus from './eventBus';

Vue.use(IdleVue, {
  eventEmitter: eventBus, idleTime: 300000,
});
Vue.use(ModalPlugin);
Vue.use(CollapsePlugin);
Vue.use(VueRouter);

new Vue({
  router,
  store,
  created() {
    eventBus.$on('logout', () => {
      if (this.$route.path !== '/login') {
        this.$router.push('/login');
      }
    });

    axios.interceptors.response.use((response) => response, (error) => {
      if (error.response.status === 500) {
        return Promise.reject(error);
      }
      if (error.response.status === 401) {
        this.$store.dispatch('logout');
      }
      return Promise.reject(error);
    });
  },
  render: (h) => h(App),
  onIdle: () => {
    store.dispatch('logout');
  },
}).$mount('#app');
