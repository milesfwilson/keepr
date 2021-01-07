<template>
  <div class="create-vault-component">
    <button class="btn mx-3" data-toggle="modal" data-target="#createVaultModal">
      <i class="fa fa-plus text-muted fa-2x" aria-hidden="true"></i>
    </button>

    <!-- Modal -->
    <div class="modal fade"
         id="createVaultModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Create Vault
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form action="" @submit.prevent="create(state.newVault, profile.id)">
              <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.newVault.name" placeholder="Vault Name" required>
              <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.newVault.description" placeholder="Vault Description" required>
              <label for="" class="small mr-3">Private?</label>
              <input class=" p-2 my-1" type="checkbox" v-model="state.newVault.isPrivate">
              <button class="btn btn-outline-dark btn-block radius">
                Create
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { notificationService } from '../services/NotificationService'
export default {
  name: 'CreateVaultComponent',
  setup() {
    const state = reactive({
      newVault: {
        name: null,
        description: null,
        isPrivate: false
      }
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      create(newVault, profileId) {
        vaultsService.create(newVault, profileId)
        $('#createVaultModal').modal('hide')
        state.newVault = {}
        notificationService.success()
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
