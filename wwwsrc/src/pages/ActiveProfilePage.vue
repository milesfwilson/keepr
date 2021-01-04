<template>
  <div class="active-profile-page">
    <div class="row my-3 py-5" v-if="activeProfile && profile">
      <div class="col-6 d-flex justify-content-end">
        <img :src="activeProfile.picture" class="align-self-center rounded-circle shadow border-big" height="200" alt="">
      </div>
      <div class="col-6 d-flex flex-column justify-content-center">
        <h1>
          {{ activeProfile.email.split('@').splice(0,1).join('') }}
        </h1>
        <p class="small">
          Vaults: {{ vaults.length }}
        </p>
        <p class="small">
          Keeps: {{ keeps.length }}
        </p>
      </div>
    </div>
    <div class="row p-3">
      <div class="col-12 d-flex">
        <h3 class="my-auto">
          Vaults
        </h3>
        <create-vault-component />
      </div>
    </div>
    <div class="row px-5">
      <vault-component v-for="vault in vaults" :key="'vault'+vault.id" :vault-props="vault" />
    </div>
    <div class="row p-3">
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
import CreateVaultComponent from '../components/CreateVaultComponent.vue'
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
      vaults: computed(() => AppState.profileVaults.filter(v => v.creatorId === route.params.profileId)),
      keeps: computed(() => AppState.profileKeeps.filter(k => k.creatorId === route.params.profileId)),
      activeProfile: computed(() => AppState.activeProfile),
      profile: computed(() => AppState.profile)
    }
  },
  components: { CreateKeepComponent, KeepComponent, CreateVaultComponent }
}
</script>

<style lang="scss" scoped>

</style>
