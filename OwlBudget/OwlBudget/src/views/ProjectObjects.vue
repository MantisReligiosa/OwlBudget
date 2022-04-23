<template>
  <b-container
    id="projectObjects"
    class="fill-height d-flex flex-column"
    fluid
  >
    <c-project-form-header
      backward="/home/project/params"
      forward="/home/project/drillingOrder"
      icon="grid"
      title="Объекты"
    />
    <b-row class="w-100 mw-100">
      <b-nav
        class="ml-3 col-md-11"
        tabs
      >
        <b-nav-item
          v-for="lot in lots"
          :key="lot.id"
          :active="lot.active"
          @click="selectLot(lot)"
        >
          <div class="d-flex">
            <c-inline-input
              v-if="lot.active"
              :object-id="lot.id"
              :patching-url="patchLotName"
              :value="lot.name"
              @value-changed="onLotNameChanged"
            />
            <span v-else>{{ lot.name }}</span>
            <b-overlay
              :show="lot.isDeleteBusy"
              class="d-inline-block"
              spinner-small
              spinner-variant="danger"
            >
              <b-icon-trash
                aria-hidden="true"
                variant="danger"
                @click.stop="deleteLot(lot)"
              />
            </b-overlay>
          </div>
        </b-nav-item>
        <b-nav-form>
          <b-overlay
            :show="isNewLotBusy"
            class="d-inline-block"
            spinner-small
            spinner-variant="success"
          >
            <b-button
              variant="outline-light"
              @click="newLot"
            >
              <span style="color: #495057">
                Добавить лот
              </span>
              <b-icon-plus-circle
                aria-hidden="true"
                variant="success"
              />
            </b-button>
          </b-overlay>
        </b-nav-form>
      </b-nav>
    </b-row>
    <div
      v-if="lots.length>0"
      class="mt-0 pt-4 pl-1 objectsContent fill-height"
    >
      <b-row class="h-100">
        <b-col
          class="h-100"
          md="3"
        >
          <b-container fluid>
            <b-row>
              <b-input-group>
                <b-overlay
                  :show="isNewClusterBusy"
                  class="d-inline-block"
                  spinner-small
                  spinner-variant="success"
                >
                  <b-button
                    class="w-100"
                    variant="outline-secondary"
                    @click="newCluster"
                  >
                    Добавить куст
                    <b-icon-plus-circle
                      aria-hidden="true"
                      variant="success"
                    />
                  </b-button>
                </b-overlay>
              </b-input-group>
            </b-row>
            <b-row>
              <b-list-group
                class="w-100"
                style="overflow-y: auto; max-height: 729px;"
              >
                <b-list-group-item
                  v-for="cluster in selectedLot.clusters"
                  :key="cluster.id"
                  button
                  :variant="cluster.active?'secondary':''"
                  @click.stop="selectCluster(cluster)"
                >
                  <div class="d-flex">
                    <c-inline-input
                      v-if="cluster.active"
                      :object-id="cluster.id"
                      :patching-url="patchClusterName"
                      :value="cluster.name"
                      @value-changed="onClusterNameChanged"
                    />
                    <div v-else>
                      <span>{{ cluster.name }}</span>
                    </div>
                    <div class="ml-auto">
                      <b-overlay
                        :show="cluster.isDeleteBusy"
                        class="d-inline-block"
                        spinner-small
                        spinner-variant="danger"
                      >
                        <b-button
                          size="sm"
                          variant="outline-danger"
                          @click.stop="deleteCluster(cluster)"
                        >
                          <b-icon-trash
                            aria-hidden="true"
                          />
                        </b-button>
                      </b-overlay>
                    </div>
                  </div>
                </b-list-group-item>
              </b-list-group>
            </b-row>
          </b-container>
        </b-col>
        <b-col
          v-if="selectedLot && selectedLot.clusters && selectedLot.clusters.length>0"
          md="8"
        >
          <b-container fluid>
            <b-row>
              <b-col md="6">
                <b-input-group prepend="Тип скважин">
                  <c-catalog
                    :get-catalog-item-url="getWellTypeCatalogItem"
                    :get-items-url="geWellTypesCatalog"
                    :item-id="selectedCluster.typeId"
                    :object-id="selectedCluster.id"
                    :patching-url="patchClusterWellType"
                    catalog-title="Справочник типов скважин"
                    @item-selected="onClusterWellTypeChanged"
                  />
                </b-input-group>
              </b-col>
              <b-col>
                <b-input-group>
                  <b-overlay
                    :show="isNewWellBusy"
                    class="d-inline-block"
                    spinner-small
                    spinner-variant="success"
                  >
                    <b-button
                      class="ml-auto"
                      variant="outline-secondary"
                      @click="newWell"
                    >
                      Добавить скважину
                      <b-icon-plus-circle
                        aria-hidden="true"
                        variant="success"
                      />
                    </b-button>
                  </b-overlay>
                </b-input-group>
              </b-col>
            </b-row>
            <b-row style="max-height: 730px; overflow: auto;">
              <b-table
                :fields="tableFields"
                :items="!selectedCluster?[]:selectedCluster.wells"
              >
                <template #cell(name)="data">
                  <div class="d-flex">
                    <c-inline-input
                      :object-id="data.item.id"
                      :patching-url="patchWellName"
                      :value="data.item.name"
                      @value-changed="function(name){onWellNameChanged(data.item.id,name)}"
                    />
                    <b-overlay
                      :show="data.item.isDeleteBusy"
                      class="d-inline-block"
                      spinner-small
                      spinner-variant="danger"
                    >
                      <b-icon-trash
                        aria-hidden="true"
                        role="button"
                        variant="danger"
                        @click="deleteWell(data.item)"
                      />
                    </b-overlay>
                  </div>
                </template>
                <template #cell(construction)="data">
                  <c-catalog
                    :get-catalog-item-url="getWellConstructionCatalogItem"
                    :get-items-url="getWellConstructionCatalog"
                    :item-id="data.item.constructionId"
                    :object-id="data.item.id"
                    :patching-url="patchWellConstruction"
                    catalog-title="Справочник конструкций скважин"
                    @item-selected="function(item){onWellConstructionChanged(data.item.id,item.id)}"
                  />
                </template>
                <template #cell(drillingRig)="data">
                  <c-catalog
                    :get-catalog-item-url="getDrillingRigCatalogItem"
                    :get-items-url="getDrillingRigCatalog"
                    :item-id="findDrillingRigId(data.item)"
                    :object-id="{wellid:data.item.id,scenarioid:selectedScenario.id}"
                    :patching-url="patchWellDrillingRig"
                    catalog-title="Справочник буровых установок"
                    @item-selected="function(item){onBindingDrillingRigToWellChanged(data.item.id,item.id)}"
                  />
                </template>
              </b-table>
            </b-row>
          </b-container>
        </b-col>
      </b-row>
    </div>
  </b-container>
</template>

<script>
import axios from 'axios';
import url from '../url.js';

export default {
  components: {
    'c-project-form-header': () => import('./ProjectFormHeader'),
    'c-inline-input': () => import('./Components/InlineInput'),
    'c-catalog': () => import('./Components/Catalog'),
    BTable: () => import('bootstrap-vue').then((m) => m.BTable),
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    /* eslint-enable vue/no-unused-components */
    BIconTrash: () => import('bootstrap-vue').then((m) => m.BIconTrash),
    BIconPlusCircle: () => import('bootstrap-vue').then((m) => m.BIconPlusCircle),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
    BInputGroup: () => import('bootstrap-vue').then((m) => m.BInputGroup),
    BCol: () => import('bootstrap-vue').then((m) => m.BCol),
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
    BListGroup: () => import('bootstrap-vue').then((m) => m.BListGroup),
    BListGroupItem: () => import('bootstrap-vue').then((m) => m.BListGroupItem),
    BNavItem: () => import('bootstrap-vue').then((m) => m.BNavItem),
    BNavForm: () => import('bootstrap-vue').then((m) => m.BNavForm),
    BNav: () => import('bootstrap-vue').then((m) => m.BNav),
  },
  data() {
    return {
      getWellTypeCatalogItem: url.getWellTypeCatalogItem,
      geWellTypesCatalog: url.geWellTypeCatalog,
      getWellConstructionCatalogItem: url.getWellConstructionCatalogItem,
      getWellConstructionCatalog: url.getWellConstructionCatalog,
      getDrillingRigCatalogItem: url.getDrillingRigCatalogItem,
      getDrillingRigCatalog: url.getDrillingRigCatalog,
      patchLotName: url.patchLotName,
      patchClusterName: url.patchClusterName,
      patchClusterWellType: url.patchClusterWellType,
      patchWellName: url.patchWellName,
      patchWellConstruction: url.patchWellConstruction,
      patchWellDrillingRig: url.patchWellDrillingRig,
      lots: [],
      selectedLot: {},
      selectedCluster: null,
      tableFields: [
        {
          key: 'name',
          label: 'Наименование',
        },
        {
          key: 'construction',
          label: 'Конструкция скважины',
        },
        {
          key: 'drillingRig',
          label: 'Буровая установка',
        },
      ],
      isNewLotBusy: false,
      isNewClusterBusy: false,
      isNewWellBusy: false,
    };
  },
  computed: {
    selectedScenario() {
      return this.$store.getters.selectedScenario;
    },
  },
  created() {
    this.reloadObjects();
  },
  methods: {
    reloadObjects() {
      return new Promise((resolve) => {
        const config = url.getProjectObjects;
        config.params = {
          id: this.$store.getters.projectId,
        };
        axios(config)
          .then((resp) => {
            this.lots = resp.data.map((lot) => {
              lot.isDeleteBusy = false;
              lot.clusters = lot.clusters.map((cluster) => {
                cluster.isDeleteBusy = false;
                cluster.wells = cluster.wells.map((well) => {
                  well.isDeleteBusy = false;
                  return well;
                });
                return cluster;
              });
              return lot;
            });
            let selectedLot = null;
            if (this.selectedLot.id) {
              selectedLot = this.lots.find((lot) => lot.id === this.selectedLot.id);
            }
            if (this.selectedLot.id && selectedLot) {
              this.selectLot(selectedLot);
            } else if (this.lots.length) {
              this.selectLot(this.lots[0]);
            }
            resolve(resp);
          });
      });
    },
    newLot() {
      this.isNewLotBusy = true;
      const lotsBefore = this.lots;
      const config = url.newLot;
      config.params = {
        projectId: this.$store.getters.projectId,
      };
      axios(config)
        .then(() => {
          this.isNewLotBusy = false;
          this.reloadObjects().then(() => {
            const lotsAfter = this.lots;
            const newLot = lotsAfter.filter((x) => lotsBefore.findIndex((y) => y.id === x.id) === -1)[0];
            this.selectLot(newLot);
          });
        });
    },
    selectLot(lot) {
      this.selectedLot = lot;
      this.lots.forEach((element) => element.active = (element === lot));
      if (lot.clusters.length) {
        this.selectCluster(lot.clusters[0]);
      }
    },
    deleteLot(lot) {
      lot.isDeleteBusy = true;
      this.$bvModal.msgBoxConfirm(`Подтвердите удаление лота "${lot.name}"`, {
        title: 'Внимание',
        size: 'sm',
        buttonSize: 'sm',
        okVariant: 'danger',
        okTitle: 'OK',
        cancelTitle: 'Cancel',
        footerClass: 'p-2',
        hideHeaderClose: false,
        centered: true,
      }).then((value) => {
        if (!value) {
          lot.isDeleteBusy = false;
          return;
        }
        const index = this.lots.indexOf(lot);
        const wasActive = lot.active;
        const config = url.deleteLot;
        config.params = {
          id: lot.id,
        };
        axios(config)
          .then(() => {
            this.reloadObjects().then(() => {
              const { lots } = this;
              if (lots.length === 0) {
                return;
              }
              if (!wasActive) {
                return;
              }
              if (index === lots.length) {
                this.selectLot(lots[index - 1]);
              } else {
                this.selectLot(lots[index]);
              }
            });
          });
      });
    },
    onLotNameChanged(newName) {
      const lot = this.lots.find((l) => l.id === this.selectedLot.id);
      lot.name = newName;
    },
    newCluster() {
      this.isNewClusterBusy = true;
      const clustersBefore = this.selectedLot.clusters;
      const config = url.newCluster;
      config.params = {
        lotId: this.selectedLot.id,
      };
      axios(config)
        .then(() => {
          this.isNewClusterBusy = false;
          this.reloadObjects().then(() => {
            const newCluster = this.selectedLot.clusters.filter((x) => clustersBefore.findIndex((y) => y.id === x.id) === -1)[0];
            this.selectCluster(newCluster);
          });
        });
    },
    selectCluster(cluster) {
      this.selectedCluster = cluster;
      if (!cluster.wells) {
        cluster.wells = [];
      }
      this.wells = cluster.wells;
      this.selectedLot.clusters.forEach((element) => element.active = (element === cluster));
    },
    onClusterNameChanged(newName) {
      const cluster = this.selectedLot.clusters.find((c) => c.id === this.selectedCluster.id);
      cluster.name = newName;
    },
    onClusterWellTypeChanged(newType) {
      const cluster = this.selectedLot.clusters.find((c) => c.id === this.selectedCluster.id);
      cluster.typeId = newType.id;
    },
    deleteCluster(cluster) {
      cluster.isDeleteBusy = true;
      this.$bvModal.msgBoxConfirm(`Подтвердите удаление куста "${cluster.name}"`, {
        title: 'Внимание',
        size: 'sm',
        buttonSize: 'sm',
        okVariant: 'danger',
        okTitle: 'OK',
        cancelTitle: 'Cancel',
        footerClass: 'p-2',
        hideHeaderClose: false,
        centered: true,
      }).then((value) => {
        if (!value) {
          cluster.isDeleteBusy = false;
          return;
        }
        const index = this.selectedLot.clusters.indexOf(cluster);
        const wasActive = cluster.active;
        const config = url.deleteCluster;
        config.params = {
          id: cluster.id,
        };
        axios(config)
          .then(() => {
            this.reloadObjects().then(() => {
              const { clusters } = this.selectedLot;
              if (clusters.length === 0) {
                return;
              }
              if (!wasActive) {
                return;
              }
              if (index === clusters.length) {
                this.selectLot(clusters[index - 1]);
              } else {
                this.selectCluster(clusters[index]);
              }
            });
          });
      });
    },
    newWell() {
      this.isNewWellBusy = true;
      const config = url.newWell;
      config.params = {
        clusterId: this.selectedCluster.id,
      };
      axios(config)
        .then(() => {
          this.isNewWellBusy = false;
          this.reloadObjects();
        });
    },
    onWellNameChanged(wellId, newName) {
      const well = this.selectedCluster.wells.find((w) => w.id === wellId);
      well.name = newName;
    },
    onWellConstructionChanged(wellId, constructionId) {
      const well = this.selectedCluster.wells.find((w) => w.id === wellId);
      well.constructionId = constructionId;
    },
    deleteWell(well) {
      well.isDeleteBusy = true;
      this.$bvModal.msgBoxConfirm(`Подтвердите удаление скважины "${well.name}"`, {
        title: 'Внимание',
        size: 'sm',
        buttonSize: 'sm',
        okVariant: 'danger',
        okTitle: 'OK',
        cancelTitle: 'Cancel',
        footerClass: 'p-2',
        hideHeaderClose: false,
        centered: true,
      }).then((value) => {
        if (!value) {
          well.isDeleteBusy = false;
          return;
        }
        const config = url.deleteWell;
        config.params = {
          id: well.id,
        };
        axios(config)
          .then(() => {
            this.reloadObjects();
          });
      });
    },
    findDrillingRigId(well) {
      const drillingRigToWell = well.drillingRigsToWell.find((d) => d.scenarioId === this.selectedScenario.id);
      if (!drillingRigToWell) {
        return null;
      }
      return drillingRigToWell.drillingRigId;
    },
    onBindingDrillingRigToWellChanged(wellId, drillingRigId) {
      const well = this.selectedCluster.wells.find((w) => w.id === wellId);
      const drillingRigToWell = well.drillingRigsToWell.find((d) => d.scenarioId === this.selectedScenario.id);
      drillingRigToWell.drillingRigId = drillingRigId;
    },
  },
};
</script>
