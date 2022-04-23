<template>
  <b-container
    id="project"
    class="fill-height d-flex flex-column pl-0"
    fluid
  >
    <b-row class="fill-height container-fluid">
      <div class="col-2 projectNav px-0">
        <div>
          <b-icon
            class="ml-4"
            icon="briefcase-fill"
          />
          <span class="projectName user-select-none ml-1">
            {{ name }}
          </span>
        </div>
        <div
          class="accordion mh-100"
          role="tablist"
        >
          <b-card
            v-for="group in groups"
            :key="group.id"
            no-body
          >
            <b-card-header
              class="p-0"
              header-tag="header"
              role="tab"
            >
              <b-button
                v-b-toggle="group.id"
                block
                class="d-flex"
                squared
                variant="secondary"
              >
                <b-icon
                  :icon="group.icon"
                  aria-hidden="true"
                  class="ml-2 mr-2 groupIcon"
                />
                {{ group.caption }}
              </b-button>
            </b-card-header>
            <b-collapse
              :id="group.id"
              accordion="my-accordion"
              role="tabpanel"
              visible
            >
              <b-button
                v-for="item in group.items"
                :key="item.id"
                :to="{path:item.route}"
                block
                class="d-flex"
                squared
                variant="light"
              >
                <b-icon
                  :icon="item.icon"
                  aria-hidden="true"
                  class="ml-2 mr-2"
                />
                {{ item.caption }}
              </b-button>
            </b-collapse>
          </b-card>
        </div>
      </div>
      <div class="col-10 fill-height d-flex flex-column">
        <router-view />
      </div>
    </b-row>
  </b-container>
</template>

<script>
export default {
  components: {
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BCard: () => import('bootstrap-vue').then((m) => m.BCard),
    BCardHeader: () => import('bootstrap-vue').then((m) => m.BCardHeader),
    BButton: () => import('bootstrap-vue').then((m) => m.BButton),
    /* eslint-disable vue/no-unused-components */
    BIcon: () => import('bootstrap-vue').then((m) => m.BIcon),
    BIconArchiveFill: () => import('bootstrap-vue').then((m) => m.BIconArchiveFill),
    BIconCardHeading: () => import('bootstrap-vue').then((m) => m.BIconCardHeading),
    BIconGrid: () => import('bootstrap-vue').then((m) => m.BIconGrid),
    BIconCardList: () => import('bootstrap-vue').then((m) => m.BIconCardList),
    BIconCalendar2Range: () => import('bootstrap-vue').then((m) => m.BIconCalendar2Range),
    BIconFilterCircle: () => import('bootstrap-vue').then((m) => m.BIconFilterCircle),
    BIconCalculator: () => import('bootstrap-vue').then((m) => m.BIconCalculator),
    BIconInboxesFill: () => import('bootstrap-vue').then((m) => m.BIconInboxesFill),
    BIconXDiamondFill: () => import('bootstrap-vue').then((m) => m.BIconXDiamondFill),
    BIconCash: () => import('bootstrap-vue').then((m) => m.BIconCash),
    BIconBook: () => import('bootstrap-vue').then((m) => m.BIconBook),
    BIconWallet: () => import('bootstrap-vue').then((m) => m.BIconWallet),
    BIconClipboardData: () => import('bootstrap-vue').then((m) => m.BIconClipboardData),
    BIconEnvelopeOpen: () => import('bootstrap-vue').then((m) => m.BIconEnvelopeOpen),
    BIconPencilSquare: () => import('bootstrap-vue').then((m) => m.BIconPencilSquare),
    BIconToggles: () => import('bootstrap-vue').then((m) => m.BIconToggles),
    /* eslint-enable vue/no-unused-components */
  },
  data() {
    return {
      groups: [
        {
          id: 'accordion-1',
          caption: 'Проект',
          icon: 'archive-fill',
          items: [
            { caption: 'Параметры', icon: 'card-heading', route: '/home/project/params' },
            { caption: 'Объекты', icon: 'grid', route: '/home/project/objects' },
            { caption: 'Порядок бурения', icon: 'card-list', route: '/home/project/drillingOrder' },
            {
              caption: 'Шаблоны графиков',
              icon: 'calendar2-range',
              route: '/home/project/constructionSchedule',
            },
            { caption: 'КНБК', icon: 'filter-circle' },
            { caption: 'Формирование', icon: 'calculator' },
          ],
        },
        {
          id: 'accordion-2',
          caption: 'Документы',
          icon: 'inboxes-fill',
          items: [
            { caption: 'Тип бюджета', icon: 'x-diamond-fill' },
            { caption: 'Параметры', icon: 'card-heading' },
            { caption: 'Расходы', icon: 'cash' },
            { caption: 'Шаблоны ЛСР', icon: 'book' },
            { caption: 'Выручка', icon: 'wallet' },
            { caption: 'Финансово-экономические показатели', icon: 'clipboard-data' },
            { caption: 'Рассылка', icon: 'envelope-open' },
          ],
        },
        {
          id: 'accordion-3',
          caption: 'ЛСР',
          icon: 'inboxes-fill',
          items: [
            { caption: 'Заполнение', icon: 'pencil-square' },
            { caption: 'Ранжирование', icon: 'toggles' },
          ],
        },
      ],
    };
  },
  computed: {
    name() {
      return this.$store.getters.projectName;
    },
  },
};
</script>
