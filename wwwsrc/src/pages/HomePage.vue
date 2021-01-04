<template>
  <div class="home-page">
    <div class="row p-5">
      <div class="card-columns">
        <keep-component v-for="keep in keeps" :key="keep.id" :keep-props="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed } from 'vue'
export default {
  name: 'HomePage',
  setup() {
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
