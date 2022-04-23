<template>
  <div
    id="home"
    class="mh-100 h-100 d-flex flex-column"
  >
    <b-navbar
      class="pl-3 border-bottom"
      toggleable="lg"
    >
      <b-navbar-brand to="/">
        <b-card-text class="logo">
          <img
            :src="logo"
            alt="logo"
          >
          <span class="brand user-select-none">
            Owl<span>Budget</span>
          </span>
        </b-card-text>
      </b-navbar-brand>
      <b-navbar-toggle target="nav-collapse" />
      <b-collapse
        id="nav-collapse"
        is-nav
      >
        <b-navbar-nav class="ml-auto">
          <b-nav-item
            title="Настройки"
            to="/home/settings"
          >
            <b-icon-gear />
          </b-nav-item>
          <b-nav-item
            v-b-modal.about
            title="О программе"
          >
            <BIconInfoCircle />
          </b-nav-item>
          <b-nav-item-dropdown
            :title="userName"
            right
          >
            <template #button-content>
              <b-icon-person-square />
            </template>
            <b-nav-text>
              <div class="px-2">
                <b-icon-person-square
                  class="pl-2"
                  font-scale="1.5"
                />
                <label>{{ userName }}</label>
              </div>
            </b-nav-text>
            <b-nav-item
              href="#"
              @click="logout"
            >
              <b-icon-box-arrow-right
                class="pl-2"
                font-scale="1.5"
              />
              <span>Выход</span>
            </b-nav-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
    <router-view />
    <b-modal
      id="about"
      ok-only
      title="О программе"
    >
      <p class="my-4">
        Hello from modal!
      </p>
    </b-modal>
  </div>
</template>
<script>
import logo from '../../public/logo.png';
import eventBus from '../eventBus';

export default {
  components: {
    BNavbar: () => import('bootstrap-vue').then((m) => m.BNavbar),
    BNavbarBrand: () => import('bootstrap-vue').then((m) => m.BNavbarBrand),
    BNavbarToggle: () => import('bootstrap-vue').then((m) => m.BNavbarToggle),
    BNavbarNav: () => import('bootstrap-vue').then((m) => m.BNavbarNav),
    BNavItem: () => import('bootstrap-vue').then((m) => m.BNavItem),
    BNavItemDropdown: () => import('bootstrap-vue').then((m) => m.BNavItemDropdown),
    BNavText: () => import('bootstrap-vue').then((m) => m.BNavText),
    BCardText: () => import('bootstrap-vue').then((m) => m.BCardText),
    BCollapse: () => import('bootstrap-vue').then((m) => m.BCollapse),
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    /* eslint-enable vue/no-unused-components */
    BIconGear: () => import('bootstrap-vue').then((m) => m.BIconGear),
    BIconInfoCircle: () => import('bootstrap-vue').then((m) => m.BIconInfoCircle),
    BIconPersonSquare: () => import('bootstrap-vue').then((m) => m.BIconPersonSquare),
    BIconBoxArrowRight: () => import('bootstrap-vue').then((m) => m.BIconBoxArrowRight),
    BModal: () => import('bootstrap-vue').then((m) => m.BModal),
  },
  data() {
    return {
      logo,
    };
  },
  computed: {
    userName() {
      return this.$store.getters.user.name;
    },
  },
  created() {
    eventBus.$on('logout', () => {
      console.log('logout emitted - App is closing modals');
      this.$bvModal.hide('about');
    });
  },
  methods: {
    logout() {
      this.$store.dispatch('logout');
    },
  },
};
</script>
