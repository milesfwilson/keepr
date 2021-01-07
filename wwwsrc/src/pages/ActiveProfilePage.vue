<template>
  <div class="active-profile-page">
    <div class="row py-4" v-if="activeProfile && profile">
      <div class="col-md-4 col-12 d-flex justify-content-center">
        <div class="text-center shadow radius  p-3 bg-light border text-dark">
          <img :src="activeProfile.picture" class="align-self-center rounded-circle shadow border-big" height="200" alt="">
          <div class="pt-2">
            <h1>
              {{ activeProfile.email.split('@').splice(0,1).join('') }}
            </h1>
            <div class="d-flex justify-content-around">
              <p class="small p-2 radius  bg-light ">
                Keeps: {{ keeps.length }}
              </p>
              <p class="small p-2 radius  bg-light ">
                Vaults: {{ vaults.length }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-8 col-12 d-flex flex-column justify-content-center">
        <div class="d-flex">
          <h3 class="my-auto">
            Vaults
          </h3>
          <create-vault-component />
        </div>
        <div class="row d-flex justify-content-around px-5">
          <vault-component v-for="vault in vaults" :key="'vault'+vault.id" :vault-props="vault" />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-3"></div>
      <div class="col-6">
        <hr>
      </div>
      <div class="col-3"></div>
    </div>
    <div class="row p-3">
      <div class="col-12 d-flex">
        <h3 class="my-auto">
          Keeps
        </h3>
        <create-keep-component />
      </div>
    </div>
    <div class="row px-5">
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
