<template>
  <div class="keep-component card radius bg-transparent border-0">
    <router-link :to="{name: 'ActiveKeep', params: {keepId: keep.id}}" class="text-dark " @click="setActiveKeep(keep)">
      <img class="card-img-top radius  " :src="keep.img" alt="Card image">
      <div class="card-img-overlay d-flex justify-content-between flex-column card-hover">
        <h4 class="card-title text-center">
          {{ keep.name }}
        </h4>
        <div class="d-flex justify-content-end">
          <img :src="keep.creator.picture" class="rounded-circle shadow" height="40">
        </div>
      </div>
    </router-link>
  </div>
</template>

<script>

import { AppState } from '../AppState'
import { computed } from 'vue'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'KeepComponent',
  props: ['keepProps'],
  setup(props) {
    return {
      keep: computed(() => props.keepProps),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.profile.id)),
      async setActiveKeep(keep) {
        keepsService.setActiveKeep(keep)
      },
      async deleteKeep(keepId) {
        keepsService.deleteKeep(keepId)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.large {
  height: 50vh;
}

.modal-img {
  background-size: cover;
  background-position: center;
}

.card-hover:hover {
background: rgba(0, 0, 0, 0.479);
color: rgba(255, 255, 255, 0.774);
border-radius: 15px;
transition: all .3s ease-in-out;

}

.card-hover {
  color: rgba(255, 255, 255, 0);
}

button {
  z-index: 2;
}
</style>
