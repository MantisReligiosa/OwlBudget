<template>
  <div
    id="app"
    class="min-vh-100 vh-100"
  >
    <router-view />
    <b-alert
      :show="dismissCountDown"
      :variant="variant"
      class="position-fixed fixed-bottom m-0 rounded-0"
      dismissible
      fade
      style="z-index: 2000;"
      @dismiss-count-down="countDownChanged"
    >
      {{ message }}
    </b-alert>
  </div>
</template>

<script>
import eventBus from './eventBus';

export default {
  components: {
    BAlert: () => import('bootstrap-vue').then((module) => module.BAlert),
  },
  data() {
    return {
      dismissSecs: 5,
      dismissCountDown: 0,
      showDismissibleAlert: false,
      variant: 'danger',
      message: '!!!!',
    };
  },
  created() {
    eventBus.$on('message', (payload) => {
      this.message = `${payload.title}: ${payload.content}`;
      this.variant = payload.variant;
      this.showAlert();
    });
  },
  methods: {
    countDownChanged(dismissCountDown) {
      this.dismissCountDown = dismissCountDown;
    },
    showAlert() {
      this.dismissCountDown = this.dismissSecs;
    },
  },
};
</script>
