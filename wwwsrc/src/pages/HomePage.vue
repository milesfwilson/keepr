<template>
  <div class="home-page">
    <div class="row">
      <div class="col-md-1 col-12"></div>
      <div class="col-12 col-md-10">
        <input type="text" class="p-2 form-control w-100" v-model="state.query.name">
      </div>
      <div class="col-md-1 col-12"></div>
    </div>
    <div class="row p-5">
      <div class="card-columns">
        <keep-component v-for="keep in keeps" :key="keep.id" :keep-props="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  name: 'HomePage',
  setup() {
    const state = reactive({
      query: {
        name: ''
      }
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps.filter(k => k.name.toUpperCase().includes(state.query.name.toUpperCase()) || k.tags.toUpperCase().includes(state.query.name.toUpperCase())))
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
