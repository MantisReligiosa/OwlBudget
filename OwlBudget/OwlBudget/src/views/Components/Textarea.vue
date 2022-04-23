<template>
  <b-form-group
    :label="caption"
    :label-for="uid"
  >
    <b-overlay
      :show="isPreloaderVisible"
      spinner-small
      spinner-variant="primary"
    >
      <b-form-textarea
        :id="uid"
        v-model="model"
        :placeholder="caption"
        :rows="rows"
        debounce="1000"
        no-resize
        @update="onInput"
      />
    </b-overlay>
  </b-form-group>
</template>

<script>
import axios from 'axios';
import {v4 as uuidv4} from 'uuid';

export default {
  components: {
    BFormTextarea: () => import('bootstrap-vue').then((m) => m.BFormTextarea),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BFormGroup: () => import('bootstrap-vue').then((m) => m.BFormGroup),

  },
  props: {
    rows: {
      type: Number,
      default: 10,
    },
    caption: {
      type: String,
      required: true,
    },
    value: {
      type: String,
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
      this.patchingUrl.data = {
        objectId: this.objectId, value: this.model,
      };
      axios(this.patchingUrl)
        .then((resp) => {
          this.isPreloaderVisible = false;
          const { data } = resp;
          if (data.isSuccess) {
            this.$emit('value-changed', this.model);
          }
        });
    },
  },
};
</script>
