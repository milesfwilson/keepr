<template>
  <div class="keep-component card radius bg-transparent border-0 shadow">
    <router-link :to="{name: 'ActiveKeep', params: {keepId: keep.id}}" class="text-dark" @click="setActiveKeep(keep)">
      <img class="card-img-top radius" :src="keep.img" alt="Card image">
      <div class="card-img-overlay d-flex flex-column justify-content-end">
        <div class="d-flex justify-content-between">
          <h4 class="card-title my-auto">
            {{ keep.name }}
          </h4>

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
      setActiveKeep(keep) {
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
</style>
