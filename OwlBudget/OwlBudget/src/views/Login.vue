<template>
  <b-jumbotron class="d-flex min-vh-100">
    <b-container class="container-md col-md-3">
      <b-card header="Авторизация">
        <b-overlay
          :show="isPreloaderVisible"
          opacity="0.5"
          spinner-variant="primary"
          variant="white"
        >
          <b-form
            class="login"
            @submit.prevent="login"
          >
            <b-form-group
              id="input-group-login"
              label="Имя пользователя:"
              label-for="input-login"
            >
              <b-form-input
                id="input-login"
                v-model="user"
                required
                type="text"
              />
            </b-form-group>
            <b-form-group
              id="input-group-password"
              label="Пароль:"
              label-for="input-password"
            >
              <b-form-input
                id="input-password"
                v-model="password"
                required
                type="password"
              />
            </b-form-group>
            <b-button
              type="submit"
              variant="outline-primary"
            >
              Вход
            </b-button>
          </b-form>
        </b-overlay>
      </b-card>
    </b-container>
  </b-jumbotron>
</template>
<script>
export default {
  components: {
    BJumbotron: () => import('bootstrap-vue').then((m) => m.BJumbotron),
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
    BCard: () => import('bootstrap-vue').then((m) => m.BCard),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BForm: () => import('bootstrap-vue').then((m) => m.BForm),
    BFormGroup: () => import('bootstrap-vue').then((m) => m.BFormGroup),
    BFormInput: () => import('bootstrap-vue').then((m) => m.BFormInput),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
  },
  data() {
    return {
      user: '',
      password: '',
      isPreloaderVisible: false,
    };
  },
  methods: {
    login() {
      const { user } = this;
      const { password } = this;
      this.isPreloaderVisible = true;
      this.$store.dispatch('login', { user, password })
        .then(() => this.$router.push('/'), () => this.isPreloaderVisible = false);
    },
  },
};
</script>
