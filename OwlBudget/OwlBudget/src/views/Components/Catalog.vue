<template>
  <b-overlay
    :show="isPreloaderVisible"
    no-center
  >
    <b-input-group>
      <b-form-input
        v-model="itemName"
        :state="state"
        readonly
      >
        {{ itemName }}
      </b-form-input>
      <b-input-group-append>
        <b-button
          variant="outline-secondary"
          @click="showModal"
        >
          <b-icon-collection
            aria-hidden="true"
          />
        </b-button>
      </b-input-group-append>
    </b-input-group>
    <template #overlay>
      <b-spinner
        class="position-absolute"
        small
        style="right: .65rem; top: .66rem"
        variant="primary"
      />
    </template>
    <b-modal
      :id="uid"
      :ref="uid"
      :title="title"
      @ok="onModalOk"
    >
      <b-container fluid>
        <b-row align-h="end">
          <b-col md="6">
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
        <b-row>
          <b-table
            id="catalogTable"
            :current-page="currentPage"
            :fields="tableFields"
            :filter="filter"
            :items="getItems"
            :per-page="perPage"
            :sort-by.sync="sortBy"
            :sort-desc.sync="sortDesc"
            select-mode="single"
            selectable
            @row-selected="rowSelected"
          />
        </b-row>
        <b-row>
          <b-col md="5">
            <b-input-group prepend="Строк">
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
              :total-rows="totalRows"
              aria-controls="catalogTable"
            />
          </b-col>
        </b-row>
      </b-container>
    </b-modal>
  </b-overlay>
</template>

<script>
import axios from 'axios';
import {v4 as uuidv4} from 'uuid';
import {perPageValues} from '../../constants.js';

const defaultFields = [
  {
    key: 'name', label: 'Наименование', sortable: true, sortDirection: 'desc',
  },
];

export default {
  components: {
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BInputGroup: () => import('bootstrap-vue').then((m) => m.BInputGroup),
    BFormInput: () => import('bootstrap-vue').then((m) => m.BFormInput),
    BInputGroupAppend: () => import('bootstrap-vue').then((m) => m.BInputGroupAppend),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    /* eslint-enable vue/no-unused-components */
    BIconCollection: () => import('bootstrap-vue').then((m) => m.BIconCollection),
    BIconSearch: () => import('bootstrap-vue').then((m) => m.BIconSearch),
    BPagination: () => import('bootstrap-vue').then((m) => m.BPagination),
    BCol: () => import('bootstrap-vue').then((m) => m.BCol),
    BFormSelect: () => import('bootstrap-vue').then((m) => m.BFormSelect),
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BTable: () => import('bootstrap-vue').then((m) => m.BTable),
    BInputGroupText: () => import('bootstrap-vue').then((m) => m.BInputGroupText),
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
    BSpinner: () => import('bootstrap-vue').then((m) => m.BSpinner),
  },
  props: {
    catalogTitle: {
      type: String,
      default: 'Справочник',
    },
    getCatalogItemUrl: {
      type: Object,
      required: true,
    },
    getItemsUrl: {
      type: Object,
      required: true,
    },
    state: {
      type: Object,
      default: null,
    },
    fields: {
      type: Array,
      default: function () {
        return defaultFields;
      },
    },
    itemId: {
      type: String,
      default: null,
    },
    patchingUrl: {
      type: Object,
      required: true,
    },
    objectId: {
      type: [String, Object],
      required: true,
    },
  },
  data() {
    return {
      uid: uuidv4(),
      isPreloaderVisible: false,
      currentPage: 1,
      totalRows: 0,
      filter: '',
      sortDesc: false,
      sortBy: '',
      perPage: perPageValues[0],
      perPageItems: perPageValues,
      tableFields: this.fields,
      title: this.catalogTitle,
      itemName: '',
      selectedCatalogItem: null,
    };
  },
  watch: {
    itemId(id) {
      this.applyItemId(id);
    },
  },
  created() {
    this.applyItemId(this.itemId);
  },
  methods: {
    getItems(ctx, callback) {
      this.getItemsUrl.params = {
        currentPage: this.currentPage,
        perPage: this.perPage,
        filter: this.filter,
        sortBy: this.sortBy,
        sortDesc: this.sortDesc,
      };
      axios(this.getItemsUrl)
        .then((resp) => {
          this.totalRows = resp.data.totalRows;
          this.currentPage = resp.data.page;
          const items = resp.data.rows;
          callback(items);
        });
    },
    rowSelected(rows) {
      this.selectedCatalogItem = rows[0];
    },
    showModal() {
      this.$refs[this.uid].show();
    },
    onModalOk() {
      if (!this.selectedCatalogItem) {
        return;
      }
      this.isPreloaderVisible = true;
      this.patchingUrl.data = {
        objectId: this.objectId, value: this.selectedCatalogItem.id,
      };
      axios(this.patchingUrl)
        .then((resp) => {
          this.isPreloaderVisible = false;
          const {data} = resp;
          this.validationState = data.isSuccess;
          this.errorMessage = data.isValid ? '' : data.errorMessage;
          if (data.isSuccess) {
            this.$emit('item-selected', this.selectedCatalogItem);
          }
        });

      this.itemName = this.selectedCatalogItem.name;
    },
    applyItemId(id) {
      if (!this.itemId) {
        this.itemName = '';
        return;
      }
      this.getCatalogItemUrl.params = {id};
      axios(this.getCatalogItemUrl)
        .then((resp) => {
          if (!resp.data) {
            return;
          }
          this.itemName = resp.data.name;
        });
    },
  },
};
</script>
