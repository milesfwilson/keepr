<template>
  <div class="active-profile-page">
    <div class="row my-3" v-if="activeProfile">
      <div class="col-6 d-flex justify-content-end">
        <img :src="activeProfile.picture" class="align-self-center" alt="">
      </div>
      <div class="col-6">
        <h1>
          {{ activeProfile.name }}
        </h1>
        <h3>
          Vaults: {{ vaults.length }}
        </h3>
        <h3>
          Keeps: {{ keeps.length }}
        </h3>
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <h3 class="my-auto">
          Vaults
        </h3>
        <button class="btn mx-3">
          <i class="fa fa-plus-circle fa-2x" aria-hidden="true"></i>
        </button>
      </div>
    </div>
    <div class="row">
      <vault-component v-for="vault in vaults" :key="'vault'+vault.id" :vault-props="vault" />
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <h3 class="my-auto">
          Keeps
        </h3>
        <create-keep-component />
      </div>
    </div>
    <div class="row p-5">
      <div class="card-columns">
        <keep-component v-for="keep in keeps" :key="'keep'+keep.id" :keep-props="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import CreateKeepComponent from '../components/CreateKeepComponent.vue'
import KeepComponent from '../components/KeepComponent.vue'
export default {
  name: 'ActiveProfilePage',
  setup() {
    const route = useRoute()
    onMounted(() => {
      keepsService.getKeepsByProfile(route.params.profileId)
      vaultsService.getVaultsByProfile(route.params.profileId)
      profileService.getProfileById(route.params.profileId)
    })
    return {
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps),
      activeProfile: computed(() => AppState.activeProfile),
      profile: computed(() => AppState.profile)
    }
  },
  components: { CreateKeepComponent, KeepComponent }
}
</script>

<style lang="scss" scoped>

</style>
