<template>
  <b-container
    id="projectConstructionSchedule"
    class="fill-height d-flex flex-column"
    fluid
  >
    <c-project-form-header
      backward="/home/project/drillingOrder"
      forward="/home/project/constructionSchedule"
      icon="calendar2-range"
      title="Шаблоны графиков"
    />
    <b-table
      :busy="isBusy"
      :fields="fields"
      :items="itemsInScenario"
      class="w-100 mw-100 fill-height mt-1"
      responsive
    >
      <template #table-busy>
        <div class="text-center text-danger my-2">
          <b-spinner
            class="align-middle"
            variant="primary"
          />
        </div>
      </template>
      <template #cell(caption)="data">
        <label>{{ data.value }}</label>
      </template>
      <template #cell()="data">
        <b-overlay
          :show="data.value.isBusy"
          opacity="0.5"
          spinner-variant="primary"
        >
          <b-overlay
            v-if="data.value.valueHovered!==null"
            :opacity="0.5"
            :show="data.value.valueHovered"
            @mouseover.native="data.value.valueHovered = true"
            @mouseleave.native="data.value.valueHovered = false"
          >
            <div style="display:flex; white-space:nowrap">
              <label class="mr-1">C</label>
              <label>{{ data.value.dateStart }}</label>
            </div>
            <div style="display:flex; white-space:nowrap">
              <label class="mr-1">По</label>
              <label>{{ data.value.dateEnd }}</label>
            </div>
            <template #overlay>
              <div class="d-flex flex-row">
                <b-button
                  class="mr-1"
                  variant="primary"
                  @click="editItem(data)"
                >
                  <b-icon-pencil-square />
                </b-button>
                <b-button
                  variant="danger"
                  @click="deleteItem(data)"
                >
                  <b-icon-trash
                    aria-hidden="true"
                  />
                </b-button>
              </div>
            </template>
          </b-overlay>
          <div
            v-else
            class="align-middle"
            style="height: 3.5rem; align-items: center; display: flex;"
            @mouseleave="data.value.emptyHovered = false"
            @mouseover="data.value.emptyHovered = true"
          >
            <b-button
              :style="{visibility: data.value.emptyHovered ? 'visible' : 'hidden'}"
              class="mx-auto"
              variant="outline-light"
            >
              <b-icon-plus-circle
                aria-hidden="true"
                variant="success"
                @click="newItem(data)"
              />
            </b-button>
          </div>
        </b-overlay>
      </template>
    </b-table>
    <c-period-modal
      ref="editModal"
      @submit="onPeriodChanged"
    />
  </b-container>
</template>

<script>
import axios from 'axios';
import url from '../url.js';

export default {
  components: {
    'c-project-form-header': () => import('./ProjectFormHeader'),
    'c-period-modal': () => import('./Components/PeriodModal'),
    BTable: () => import('bootstrap-vue').then((m) => m.BTable),
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    /* eslint-enable vue/no-unused-components */
    BIconPencilSquare: () => import('bootstrap-vue').then((m) => m.BIconPencilSquare),
    BIconTrash: () => import('bootstrap-vue').then((m) => m.BIconTrash),
    BIconPlusCircle: () => import('bootstrap-vue').then((m) => m.BIconPlusCircle),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BSpinner: () => import('bootstrap-vue').then((m) => m.BSpinner),
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
  },
  data() {
    return {
      isBusy: true,
      items: [],
      fields: [],
    };
  },
  computed: {
    itemsInScenario() {
      const scenarioId = this.$store.getters.selectedScenario.id;
      return this.items.filter((s) => s.scenarioId === scenarioId);
    },
  },
  created() {
    this.reloadTable();
  },
  methods: {
    reloadTable() {
      this.isBusy = true;
      let wells = [];
      let config = url.getWellHeaders;
      config.params = {
        id: this.$store.getters.projectId,
      };
      axios(config)
        .then((resp) => {
          wells = resp.data;

          this.fields = [{
            key: 'caption',
            stickyColumn: true,
            label: 'Вид шаблона графика',
          }].concat(wells);

          config = url.getStages;
          config.params = {
            id: this.$store.getters.projectId,
          };

          axios(config)
            .then((resp1) => {
              const stages = resp1.data;
              this.items = stages.map((stage) => {
                wells.forEach((well) => {
                  if (stage[well.key]) {
                    stage[well.key].valueHovered = false;
                  } else {
                    stage[well.key] = { emptyHovered: false };
                  }
                  stage[well.key].isBusy = false;
                });
                return stage;
              });
              this.isBusy = false;
            });
        });
    },
    newItem(data) {
      const config = url.newStage;
      config.params = {
        projectId: this.$store.getters.projectId,
        scenarioId: this.$store.getters.selectedScenario.id,
        wellId: data.field.key,
        templateType: data.item.templateType,
      };
      axios(config)
        .then(() => {
          this.reloadTable();
        });
    },
    editItem(data) {
      this.$refs.editModal.show(data);
    },
    deleteItem(data) {
      const config = url.deleteStage;
      config.params = {
        id: data.value.id,
      };
      axios(config)
        .then(() => {
          this.reloadTable();
        });
    },
    onPeriodChanged(data) {
      const config = url.editStage;
      config.params = {
        id: data.context.value.id,
        dateFrom: data.dateFrom,
        timeFrom: data.timeFrom,
        dateTo: data.dateTo,
        timeTo: data.timeTo,
        templateType: data.context.item.templateType,
      };
      axios(config)
        .then(() => {
          this.reloadTable();
        });
    },
  },
};
</script>
