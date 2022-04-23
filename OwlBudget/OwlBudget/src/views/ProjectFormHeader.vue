<template>
  <div>
    <b-row>
      <b-col md="4">
        <h3>
          <b-icon
            :icon="icon"
            aria-hidden="true"
          />
          {{ title }}
        </h3>
      </b-col>
      <b-col
        class="mt-1"
        md="2"
      >
        <b-input-group
          v-if="isBudgetVisible"
          prepend="Бюджет"
        >
          <b-form-select
            v-model="selectedBudgetType"
            :options="budgets"
            text-field="name"
            value-field="value"
          />
        </b-input-group>
      </b-col>
      <b-col
        class="mt-1"
        md="3"
      >
        <b-input-group
          v-if="isScenarioVisible"
          prepend="Сценарий"
        >
          <b-form-select
            v-model="selectedScenarioId"
            :options="scenarios"
            text-field="name"
            value-field="id"
          />
          <b-input-group-append>
            <b-button v-b-modal.scenarioManager>
              <b-icon-journal-text
                aria-hidden="true"
              />
            </b-button>
          </b-input-group-append>
        </b-input-group>
      </b-col>
      <b-col
        class="d-flex"
        md="2"
      >
        <b-button
          v-if="isBackwardVisible"
          :to="backward"
          class="mt-1 ml-auto"
          variant="primary"
        >
          <b-icon-chevron-compact-left />
          Назад
        </b-button>

        <b-button
          v-if="isForwardVisible"
          :to="forward"
          class="mt-1 ml-auto"
          variant="primary"
        >
          Далее
          <b-icon-chevron-compact-right />
        </b-button>
      </b-col>
    </b-row>
    <b-modal
      id="scenarioManager"
      title="Менеджер сценариев"
    />
  </div>
</template>

<script>
import {budgetValues} from '../constants.js';

export default {
  components: {
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    BIconCardHeading: () => import('bootstrap-vue').then((m) => m.BIconCardHeading),
    BIconChevronCompactLeft: () => import('bootstrap-vue').then((m) => m.BIconChevronCompactLeft),
    BIconChevronCompactRight: () => import('bootstrap-vue').then((m) => m.BIconChevronCompactRight),
    BIconJournalText: () => import('bootstrap-vue').then((m) => m.BIconJournalText),
    /* eslint-enable vue/no-unused-components */
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BCol: () => import('bootstrap-vue').then((m) => m.BCol),
    BInputGroup: () => import('bootstrap-vue').then((m) => m.BInputGroup),
    BFormSelect: () => import('bootstrap-vue').then((m) => m.BFormSelect),
    BInputGroupAppend: () => import('bootstrap-vue').then((m) => m.BInputGroupAppend),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
    BModal: () => import('bootstrap-vue').then((m) => m.BModal),
  },
  props: {
    icon: {
      type: String,
      default: '',
    },
    title: {
      type: String,
      default: '',
    },
    hideScenarios: {
      type: Boolean,
      default: false,
    },
    hideBudgets: {
      type: Boolean,
      default: false,
    },
    forward: {
      type: String,
      default: '/',
    },
    backward: {
      type: String,
      default: '/',
    },
    hideForward: {
      type: Boolean,
      default: false,
    },
    hideBackward: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      managerSceneList: [],
    };
  },
  computed: {
    budgets() {
      return budgetValues;
    },
    scenarios() {
      return this.$store.getters.scenarios;
    },
    isScenarioVisible() {
      return !this.hideScenarios;
    },
    isBudgetVisible() {
      return !this.hideBudgets;
    },
    isForwardVisible() {
      return !this.hideForward;
    },
    isBackwardVisible() {
      return !this.hideBackward;
    },
    selectedBudgetType: {
      get() {
        return this.$store.getters.selectedBudgetType;
      },
      set(value) {
        this.$store.dispatch('selectBudgetType', value);
      },
    },
    selectedScenarioId: {
      get() {
        return this.$store.getters.selectedScenario.id;
      },
      set(value) {
        this.$store.dispatch('selectScenarioById', value);
      },
    },
  },
};
</script>
