<template>
  <div class="home-page">
    <div class="row mt-5">
      <div class="col-sm-1 col-12"></div>
      <div class="col-12 col-sm-10 d-flex">
        <input type="text" class="p-4 form-control w-100 radius shadow-lg border-0 ml-3" placeholder="Search" v-model="state.query.name">
        <i class="fa fa-search text-muted my-auto left" aria-hidden="true"></i>
      </div>
      <div class="col-sm-1 col-12"></div>
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
import { computed, reactive, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'HomePage',
  setup() {
    const state = reactive({
      query: {
        name: ''
      }
    })
    onMounted(() => {
      keepsService.get()
    })
    return {
      state,
      sortBy: computed(() => AppState.sortBy),
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps.filter(k => k.name.toUpperCase().includes(state.query.name.toUpperCase()) || k.tags.toUpperCase().includes(state.query.name.toUpperCase())))
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.radius {
  border-radius: 25px;
}
.left {
  position: relative;
  left: -30px;
}
</style>
