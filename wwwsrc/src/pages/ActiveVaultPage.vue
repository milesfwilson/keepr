<template>
  <div class="active-vault-page">
    <div class="row my-3">
      <div class="col-12 text-center">
        <h3>
          {{ activeVault.name }}
        </h3>
        <p>
          {{ activeVault.description }}
        </p>
      </div>
    </div>
    <div class="row">
      <div class="col-3 d-flex justify-content-center">
        <button class="btn" @click="backToVaults(activeVault.creatorId)">
          <i class="fa fa-arrow-left" aria-hidden="true"></i>
        </button>
      </div>
      <div class="col-6">
        <hr>
      </div>
      <div class="col-3 d-flex justify-content-center">
        <div class=" d-flex" v-if="activeVault.creatorId == profile.id">
          <button class="btn" @click="togglePrivate(activeVault)">
            <i v-if="!activeVault.isPrivate" class="fa fa-eye" aria-hidden="true"></i>
            <i v-else class="fa fa-eye-slash" aria-hidden="true"></i>
          </button>

          <button class="btn" @click="deleteVault(activeVault.id, profile.id)">
            <i class="fa fa-trash-o text-muted" aria-hidden="true"></i>
          </button>
        </div>
      </div>
    </div>
    <div class="row p-5">
      <div class="card-columns">
        <keep-component v-for="keep in activeKeeps" :key="'keep'+keep.id" :keep-props="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import router from '../router'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { computed, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import swal from 'sweetalert'
export default {
  name: 'ActiveVaultPage',
  setup() {
    const route = useRoute()
    onMounted(() => {
      keepsService.getKeepsByVault(route.params.vaultId)
      vaultsService.getOne(route.params.vaultId)
    })
    return {
      activeVault: computed(() => AppState.activeVault),
      activeKeeps: computed(() => AppState.activeKeeps),
      profile: computed(() => AppState.profile),
      deleteVault(vaultId, profileId) {
        swal({
          title: 'Are you sure?',
          text: 'Once deleted, you will not be able to get your vault back',
          icon: 'warning',
          buttons: true,
          dangerMode: true
        })
          .then((willDelete) => {
            if (willDelete) {
              swal('Deleted!', {
                icon: 'success'
              })
              vaultsService.deleteVault(vaultId, profileId)
              router.push({ name: 'ActiveProfile', params: { profileId: profileId } })
            } else {
              swal('Your imaginary file is safe!')
            }
          })
      },
      togglePrivate(vault) {
        const editedVault = vault
        editedVault.isPrivate = !editedVault.isPrivate
        vaultsService.edit(editedVault.id, editedVault)
      },
      backToVaults(profileId) {
        router.push({ name: 'ActiveProfile', params: { profileId: profileId } })
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
