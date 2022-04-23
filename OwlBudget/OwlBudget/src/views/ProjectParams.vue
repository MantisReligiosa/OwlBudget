<template>
  <b-container
    id="projectParams"
    class="fill-height d-flex flex-column"
    fluid
  >
    <c-project-form-header
      forward="/home/project/objects"
      hide-backward
      hide-budgets
      hide-scenarios
      icon="card-heading"
      title="Параметры"
    />
    <b-row>
      <b-col md="3">
        <c-input
          :object-id="projectId"
          :patching-url="patchName"
          :value="name"
          caption="Наименование"
          @value-changed="onNameChanged"
        />

        <b-form-group
          id="fieldset-1"
          label="Тип договора"
          label-for="contractTypeId.id"
        >
          <b-form-select
            id="contractTypeId.id"
            ref="contractTypeId.id"
            v-model="contractTypeId"
            :options="contractTypes"
            text-field="description"
            value-field="id"
          />
        </b-form-group>

        <c-input
          :object-id="projectId"
          :patching-url="patchLocation"
          :value="location"
          caption="Месторождение (площадь)"
          @value-changed="onLocationChanged"
        />

        <b-form-group
          id="fieldset-3"
          invalid-feedback="Обязательное поле"
          label="Регион"
          label-for="regionId.id"
        >
          <c-catalog
            :get-catalog-item-url="getRegionCatalogItem"
            :get-items-url="getRegionCatalog"
            :item-id="regionId"
            :object-id="projectId"
            :patching-url="patchRegion"
            catalog-title="Справочник регионов"
            @item-selected="onNewRegionSelected"
          />
        </b-form-group>
      </b-col>
      <b-col md="8">
        <c-textarea
          :object-id="projectId"
          :patching-url="patchDescription"
          :rows="8"
          :value="description"
          caption="Описание"
          @value-changed="onDescriptionChanged"
        />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import axios from 'axios';
import url from '../url.js';

export default {
  components: {
    'c-project-form-header': () => import('./ProjectFormHeader'),
    'c-input': () => import('./Components/Input'),
    'c-catalog': () => import('./Components/Catalog'),
    'c-textarea': () => import('./Components/Textarea'),
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BCol: () => import('bootstrap-vue').then((m) => m.BCol),
    BFormSelect: () => import('bootstrap-vue').then((m) => m.BFormSelect),
    BFormGroup: () => import('bootstrap-vue').then((m) => m.BFormGroup),
  },
  data() {
    return {
      getRegionCatalogItem: url.getRegionCatalogItem,
      getRegionCatalog: url.getRegionCatalog,
      patchName: url.patchProjectName,
      patchLocation: url.patchProjectLocation,
      patchRegion: url.patchProjectRegion,
      patchDescription: url.patchProjectDescription,
      projectId: this.$store.getters.projectId,
      contractTypeId: null,
      regionId: null,
      location: '',
      name: '',
      contractTypes: [],
      regions: [],
      description: '',
    };
  },
  created() {
    this.name = this.$store.getters.projectName;
    axios(url.getContractTypes)
      .then((resp) => {
        if (!resp.data) {
          return;
        }
        this.contractTypes = resp.data;
      });
    const config = url.getProjectParams;
    config.params = {
      id: this.$store.getters.projectId,
    };
    axios(config)
      .then((resp) => {
        const projectParams = resp.data;
        this.contractTypeId = projectParams.contractTypeId;
        this.regionId = projectParams.regionId;
        this.location = projectParams.location;
        this.description = projectParams.description;
      });
  },
  methods: {
    onNameChanged(newName) {
      this.name = newName;
    },
    onLocationChanged(newLocation) {
      this.location = newLocation;
    },
    onNewRegionSelected(region) {
      this.regionId = region.id;
    },
    onDescriptionChanged(description) {
      this.description = description;
    },
  },
};
</script>
