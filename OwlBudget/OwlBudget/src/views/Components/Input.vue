<template>
  <b-form-group
    :invalid-feedback="errorMessage"
    :label="caption"
    :label-for="uid"
    :state="validationState"
  >
    <b-overlay
      :show="isPreloaderVisible"
      no-center
    >
      <div class="d-flex">
        <b-form-input
          :id="uid"
          :ref="uid"
          v-model="model"
          :placeholder="caption"
          :state="validationState"
          autocomplete="off"
          debounce="1000"
          @update="onInput"
        />
      </div>
      <template #overlay>
        <b-spinner
          class="position-absolute"
          small
          style="right: .65rem; top: .66rem"
          variant="primary"
        />
      </template>
    </b-overlay>
  </b-form-group>
</template>

<script>
import axios from 'axios';
import {v4 as uuidv4} from 'uuid';

export default {
  components: {
    BFormGroup: () => import('bootstrap-vue').then((m) => m.BFormGroup),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BFormInput: () => import('bootstrap-vue').then((m) => m.BFormInput),
    BSpinner: () => import('bootstrap-vue').then((m) => m.BSpinner),
  },
  props: {
    caption: {
      type: String,
      default: '',
    },
    value: {
      type: [String, Number],
      default: '',
    },
    objectId: {
      type: String,
      required: true,
    },
    patchingUrl: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      model: this.value,
      errorMessage: '',
      validationState: null,
      isPreloaderVisible: false,
      uid: uuidv4(),
    };
  },
  watch: {
    value(val) {
      this.model = val;
    },
  },
  methods: {
    onInput() {
      this.isPreloaderVisible = true;
      this.validationState = null;
      this.patchingUrl.data = {
        objectId: this.objectId, value: this.model,
      };
      axios(this.patchingUrl)
        .then((resp) => {
          this.isPreloaderVisible = false;
          const {data} = resp;
          this.validationState = data.isSuccess;
          this.errorMessage = data.isValid ? '' : data.errorMessage;
          if (data.isSuccess) {
            this.$emit('value-changed', this.model);
          }
        });
    },
  },
};
</script>
