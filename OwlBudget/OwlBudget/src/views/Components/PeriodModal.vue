<template>
  <b-modal
    id="periodModal"
    ref="periodModal"
    :title="title"
    @ok="handleModalOK"
  >
    <form @submit.stop.prevent="handleSubmit">
      <b-input-group
        class="periodInputGroup"
        prepend="С"
      >
        <b-form-datepicker
          v-model="dateFrom"
          :date-format-options="{ year: 'numeric', month: 'numeric', day: 'numeric' }"
          locale="ru"
        />
        <b-form-timepicker
          v-model="timeFrom"
          locale="ru"
          no-close-button
        />
      </b-input-group>
      <b-input-group
        v-if="context&&context.item.isDateEndEnabled"
        class="periodInputGroup"
        prepend="По"
      >
        <b-form-datepicker
          v-model="dateTo"
          :date-format-options="{ year: 'numeric', month: 'numeric', day: 'numeric' }"
          locale="ru"
        />
        <b-form-timepicker
          v-model="timeTo"
          locale="ru"
          no-close-button
        />
      </b-input-group>
    </form>
  </b-modal>
</template>

<script>
import moment from 'moment';

export default {
  components: {
    BFormTimepicker: () => import('bootstrap-vue').then((m) => m.BFormTimepicker),
    BFormDatepicker: () => import('bootstrap-vue').then((m) => m.BFormDatepicker),
    BInputGroup: () => import('bootstrap-vue').then((m) => m.BInputGroup),
  },
  data() {
    return {
      context: null,
      dateFrom: null,
      timeFrom: null,
      dateTo: null,
      timeTo: null,
    };
  },
  computed: {
    title() {
      return this.context == null ? '---' : `${this.context.field.label}: ${this.context.item.caption}`;
    },
  },
  methods: {
    handleModalOK(bvModalEvt) {
      bvModalEvt.preventDefault();
      this.handleSubmit();
    },
    show(data) {
      this.context = data;
      this.dateFrom = this.getDate(data.value.dateStart);
      this.timeFrom = this.getTime(data.value.dateStart);
      this.dateTo = this.getDate(data.value.dateEnd);
      this.timeTo = this.getTime(data.value.dateEnd);
      this.$refs.periodModal.show();
    },
    handleSubmit() {
      this.$nextTick(() => {
        this.$bvModal.hide('periodModal');
      });
      this.$emit('submit', {
        dateFrom: this.dateFrom,
        timeFrom: this.timeFrom,
        dateTo: this.dateTo,
        timeTo: this.timeTo,
        context: this.context,
      });
    },
    getDate(t) {
      return moment(t, 'DD.MM.YYYY HH:mm').format('YYYY-MM-DD');
    },
    getTime(t) {
      return moment(t, 'DD.MM.YYYY HH:mm').format('HH:mm');
    },
  },
};
</script>
