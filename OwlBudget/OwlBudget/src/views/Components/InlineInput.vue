<template>
  <b-overlay
    :show="isPreloaderVisible"
    no-center
  >
    <div class="d-flex">
      <b-form-input
        :id="uid"
        :ref="uid"
        v-model="model"
        :state="validationState"
        autocomplete="off"
        class="buildinInput"
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
    <b-tooltip
      v-if="validationState===false"
      :target="uid"
      variant="danger"
    >
      {{ errorMessage }}
    </b-tooltip>
  </b-overlay>
</template>

<script>
import axios from 'axios';
import {v4 as uuidv4} from 'uuid';

export default {
  components: {
    BFormInput: () => import('bootstrap-vue').then((m) => m.BFormInput),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BSpinner: () => import('bootstrap-vue').then((m) => m.BSpinner),
  },
  props: {
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
      /*
       * Если убрать эту проверку, то зелёных галочек не будет
       */
      if (this.model === val) {
        return;
      }
      this.model = val;
      this.validationState = null;
    },
  },
  methods: {
    onInput() {
      this.isPreloaderVisible = true;
      this.validationState = null;
      this.patchingUrl.data = {
        objectId: this.objectId,
        value: this.model,
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
