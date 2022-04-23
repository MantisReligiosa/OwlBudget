<template>
  <b-container
    id="projectObjects"
    class="fill-height d-flex flex-column"
    fluid
  >
    <c-project-form-header
      backward="/home/project/objects"
      forward="/home/project/constructionSchedule"
      icon="card-list"
      title="Порядок бурения"
    />
    <b-overlay
      :show="isPreloaderVisible"
      spinner-variant="primary"
      variant="white"
    >
      <template v-for="sequence in sequences">
        <b-row
          :key="sequence.id"
          class="w-100 mw-100 mt-1"
        >
          <b-col
            class="w-25"
            md="auto"
          >
            <b-card
              :footer="sequence.drillingRigName"
              class="text-center"
            >
              <c-inline-input
                :object-id="sequence.id"
                :patching-url="patchSequenceName"
                :value="sequence.name"
              />
            </b-card>
          </b-col>
          <b-col>
            <Container
              :get-child-payload="function(index){return getChildPayload(sequence, index)}"
              :group-name="sequence.drillingRigId"
              orientation="horizontal"
              @drop="onDrop(sequence.id, $event)"
              @drag-enter="function(){showNewZoneFor(sequence)}"
            >
              <Draggable
                v-for="well in sequence.wellsToDrillingRigSequence"
                :key="well.wellId"
              >
                <b-card class="text-center">
                  <label>
                    {{ well.wellName }}
                  </label>
                </b-card>
              </Draggable>
            </Container>
          </b-col>
        </b-row>
        <b-row
          v-if="sequence.newZoneIsVisible"
          key="newZone"
          class="w-100 mw-100 mt-1"
        >
          <b-col
            class="fill-height"
            style="border: 2px dashed lightgray; border-radius: 0.25rem; text-align:center"
          >
            <label>
              В новую последовательность
            </label>
            <Container
              :get-child-payload="function(index){return getChildPayload(sequence, index)}"
              :group-name="sequence.drillingRigId"
              class="fill-height"
              orientation="horizontal"
              @drop="onNewSequence"
            >
              <Draggable style="height: 4rem" />
            </Container>
          </b-col>
        </b-row>
      </template>
    </b-overlay>
  </b-container>
</template>

<script>
import axios from 'axios';
import url from '../url.js';

export default {
  components: {
    'c-project-form-header': () => import('./ProjectFormHeader'),
    'c-inline-input': () => import('./Components/InlineInput'),
    Draggable: () => import('vue-smooth-dnd').then((m) => m.Draggable),
    Container: () => import('vue-smooth-dnd').then((m) => m.Container),
    BCol: () => import('bootstrap-vue').then((m) => m.BCol),
    BCard: () => import('bootstrap-vue').then((m) => m.BCard),
    BRow: () => import('bootstrap-vue').then((m) => m.BRow),
    BOverlay: () => import('bootstrap-vue').then((m) => m.BOverlay),
    BContainer: () => import('bootstrap-vue').then((m) => m.BContainer),
  },
  data() {
    return {
      patchSequenceName: url.patchSequenceName,
      sequencesInScenarios: [],
      isPreloaderVisible: false,
      drillingRigIdForNewZone: 0,
    };
  },
  computed: {
    sequences() {
      const scenarioId = this.$store.getters.selectedScenario.id;
      return this.sequencesInScenarios.filter((s) => s.scenarioId === scenarioId);
    },
  },
  created() {
    this.reloadObjects();
  },
  methods: {
    reloadObjects() {
      return new Promise((resolve) => {
        this.drillingRigIdForNewZone = 0;
        const config = url.getProjectDrillingRigSequences;
        config.params = {
          id: this.$store.getters.projectId,
        };
        axios(config)
          .then((resp) => {
            this.sequencesInScenarios = resp.data.map((s) => {
              s.newZoneIsVisible = false;
              return s;
            });
            resolve(resp);
          });
      });
    },
    showNewZoneFor(sequence) {
      if (this.drillingRigIdForNewZone !== sequence.drillingRigId) {
        this.drillingRigIdForNewZone = sequence.drillingRigId;
        sequence.newZoneIsVisible = true;
      }
    },
    getChildPayload(sequence, index) {
      return sequence.wellsToDrillingRigSequence[index];
    },
    onNewSequence(dropResult) {
      if (!dropResult.addedIndex) {
        return;
      }
      this.isPreloaderVisible = true;
      const config = url.moveToNewSequence;
      config.params = {
        wellId: dropResult.payload.wellId,
        scenarioId: this.$store.getters.selectedScenario.id,
      };
      axios(config)
        .then(() => this.reloadObjects())
        .then(() => this.isPreloaderVisible = false);
    },
    onDrop(sequenceId, dropResult) {
      if (dropResult.addedIndex == null) {
        return;
      }
      this.isPreloaderVisible = true;
      const config = url.moveToSequence;
      config.data = {
        currentSequenceId: dropResult.payload.drillingRigSequenceId,
        newSequenceId: sequenceId,
        wellId: dropResult.payload.wellId,
        order: dropResult.addedIndex,
      };
      axios(config)
        .then(() => this.reloadObjects())
        .then(() => this.isPreloaderVisible = false);
    },
  },
};
</script>
