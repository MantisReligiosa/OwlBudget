<template>
  <b-container
    id="projects"
    class="fill-height d-flex flex-column"
    fluid
  >
    <b-row class="container-fluid mt-3">
      <b-col
        class="pl-0"
        md="3"
      >
        <b-overlay
          :show="isNewProjectBusy"
          class="d-inline-block"
          spinner-small
          spinner-variant="primary"
        >
          <b-button
            variant="primary"
            @click="newClick"
          >
            <b-icon-plus-square-fill
              aria-hidden="true"
            />
            Новый проект
          </b-button>
        </b-overlay>
      </b-col>
      <b-col
        class="pr-0"
        md="2"
        offset-md="7"
      >
        <b-input-group>
          <b-form-input
            v-model="filter"
            debounce="500"
            type="search"
          />
          <b-input-group-append>
            <b-input-group-text>
              <b-icon-search />
            </b-input-group-text>
          </b-input-group-append>
        </b-input-group>
      </b-col>
    </b-row>
    <b-row
      class="fill-height container-fluid mt-2"
      style="max-height: 730px; overflow-y: auto"
    >
      <b-col class="px-0">
        <b-table-simple>
          <b-thead>
            <b-tr>
              <b-th style="width:.1rem" />
              <b-th class="w-25">
                Наименование
              </b-th>
              <b-th>
                Описание
              </b-th>
            </b-tr>
          </b-thead>
          <b-tbody>
            <template v-for="yearArray in rows">
              <b-tr
                :key="yearArray.key"
                @click="toggle(yearArray)"
              >
                <b-td class="w-auto">
                  <b-icon-caret-up-fill
                    v-if="!yearArray.isOpened"
                  />
                  <b-icon-caret-down-fill
                    v-else
                  />
                </b-td>
                <b-td>
                  {{ yearArray.key }}
                </b-td>
                <b-td />
              </b-tr>
              <template v-if="yearArray.isOpened">
                <b-tr
                  v-for="item in yearArray.items"
                  :key="item.key"
                  style="cursor:pointer"
                  @click="openProject(item)"
                >
                  <b-td />
                  <b-td>
                    <div class="d-flex">
                      <span>
                        {{ item.name }}
                      </span>
                      <b-overlay
                        :show="item.isDeleting"
                        class="d-inline-block, ml-auto"
                        spinner-small
                        spinner-variant="danger"
                      >
                        <b-button
                          size="sm"
                          variant="outline-danger"
                          @click.stop="deleteProject(item)"
                        >
                          <b-icon-trash
                            aria-hidden="true"
                          />
                        </b-button>
                      </b-overlay>
                    </div>
                  </b-td>
                  <b-td>
                    {{ item.description }}
                  </b-td>
                </b-tr>
              </template>
            </template>
          </b-tbody>
        </b-table-simple>
      </b-col>
    </b-row>
    <b-row>
      <b-col md="2">
        <b-input-group
          class="mt-3"
          prepend="Строк"
        >
          <b-form-select
            v-model="perPage"
            :options="perPageItems"
          />
        </b-input-group>
      </b-col>
      <b-col>
        <b-pagination
          v-model="currentPage"
          :per-page="perPage"
          :total-rows="totalProjects"
          class="mt-3"
          @page-click="onPageClick"
        />
      </b-col>
    </b-row>
  </b-container>
</template>
<script>
import axios from 'axios';
import url from '../url.js';
import { perPageValues } from '../constants.js';

const groupProjects = function (projects) {
  const key = 'creationYear';
  return projects.reduce((groupedProjects, project) => {
    project.isDeleting = false;
    const index = groupedProjects.findIndex((element) => element.key === project[key]);
    if (index === -1) {
      groupedProjects.push({ key: project[key], items: [project], isOpened: false });
    } else {
      groupedProjects[index].items.push(project);
    }
    return groupedProjects;
  }, []);
};
export default {
  components: {
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BCol: () => import('bootstrap-vue').then((m) => m.BCol),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    /* eslint-enable vue/no-unused-components */
    BIconPlusSquareFill: () => import('bootstrap-vue').then((m) => m.BIconPlusSquareFill),
    BIconSearch: () => import('bootstrap-vue').then((m) => m.BIconSearch),
    BIconCaretUpFill: () => import('bootstrap-vue').then((m) => m.BIconCaretUpFill),
    BIconCaretDownFill: () => import('bootstrap-vue').then((m) => m.BIconCaretDownFill),
    BIconTrash: () => import('bootstrap-vue').then((m) => m.BIconTrash),
    BInputGroup: () => import('bootstrap-vue').then((m) => m.BInputGroup),
    BInputGroupAppend: () => import('bootstrap-vue').then((m) => m.BInputGroupAppend),
    BInputGroupText: () => import('bootstrap-vue').then((m) => m.BInputGroupText),
    BFormInput: () => import('bootstrap-vue').then((m) => m.BFormInput),
    BFormSelect: () => import('bootstrap-vue').then((m) => m.BFormSelect),
    BTableSimple: () => import('bootstrap-vue').then((m) => m.BTableSimple),
    BTr: () => import('bootstrap-vue').then((m) => m.BTr),
    BThead: () => import('bootstrap-vue').then((m) => m.BThead),
    BTh: () => import('bootstrap-vue').then((m) => m.BTh),
    BTbody: () => import('bootstrap-vue').then((m) => m.BTbody),
    BTd: () => import('bootstrap-vue').then((m) => m.BTd),
    BPagination: () => import('bootstrap-vue').then((m) => m.BPagination),
  },
  data() {
    return {
      currentPage: 1,
      perPage: perPageValues[1],
      perPageItems: perPageValues,
      filter: '',
      totalProjects: 0,
      rows: null,
      isNewProjectBusy: false,
    };
  },
  watch: {
    filter() {
      this.reloadList();
    },
    perPage() {
      this.reloadList();
    },
  },
  mounted() {
    this.reloadList();
  },
  methods: {
    newClick() {
      this.isNewProjectBusy = true;
      this.$store.dispatch('newProject')
        .then(() => this.$store.dispatch('loadScenarios', this.$store.getters.projectId))
        .then(() => this.$router.push('/home/project/params'));
    },
    onPageClick(bvEvent, page) {
      this.currentPage = page;
      this.reloadList();
    },
    reloadList() {
      const config = url.getProjectsList;
      config.params = {
        currentPage: this.currentPage,
        perPage: this.perPage,
        filter: this.filter,
        sortBy: this.sortBy,
        sortDesc: this.sortDesc,
      };
      axios(config)
        .then((resp) => {
          if (!resp.data) {
            return;
          }
          this.totalProjects = resp.data.totalRows;
          const projects = resp.data.rows;
          this.currentPage = resp.data.page;
          if (!projects) {
            this.rows = [];
            return;
          }
          this.rows = groupProjects(projects);
        });
    },
    toggle(yearArray) {
      const index = this.rows.indexOf(yearArray);
      this.rows[index].isOpened = !yearArray.isOpened;
    },
    openProject(project) {
      this.$store.dispatch('openProject', project)
        .then(() => this.$store.dispatch('loadScenarios', this.$store.getters.projectId))
        .then(() => this.$router.push('/home/project/params'));
    },
    deleteProject(project) {
      project.isDeleting = true;
      this.$bvModal.msgBoxConfirm(`Подтвердите удаление проекта "${project.name}"`, {
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
          project.isDeleting = false;
          return;
        }
        const config = url.deleteProject;
        config.params = {
          id: project.id,
        };
        axios(config).then(() => this.reloadList());
      });
    },
  },
};
</script>
