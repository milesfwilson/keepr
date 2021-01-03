<template>
  <div class="active-keep-component">
    {{ activeKeep.title }}
    {{ activeKeep.name }}
    <img :src="activeKeep.img" alt="">
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed } from 'vue'
export default {
  name: 'ActiveKeepComponent',

  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
