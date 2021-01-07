<template>
  <div class="keep-component card radius bg-transparent border-0">
    <!-- <router-link :to="{name: 'ActiveKeep', params: {keepId: keep.id}}" class="text-dark " @click="setActiveKeep(keep)"> -->
    <img class="card-img-top radius  " :src="keep.img" alt="Card image">
    <div class="card-img-overlay d-flex justify-content-end flex-column card-hover">
      <div class="">
        <div class="d-flex justify-content-between">
          <button type="button" class="btn text-light" data-toggle="modal" :data-target="'#keepModal'+keep.id" @click="getOne(keep.id, keep)">
            <h4 class="card-title text-center">
              {{ keep.name }}
            </h4>
          </button>
          <router-link :to="{name: 'ActiveProfile', params: {profileId: keep.creatorId}}">
            <img :src="keep.creator.picture" class="rounded-circle shadow" height="40">
          </router-link>
        </div>
      </div>
    </div>

    <!-- Modal -->
    <div class=" modal fade"
         :id="'keepModal'+keep.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-body">
            <div class="row">
              <div class="col-6">
                <img :src="keep.img" class="img-fluid" alt="">
              </div>
              <div class="col-6 d-flex flex-column justify-content-between">
                <div class="d-flex justify-content-end">
                  <button class="btn" v-if="keep.vaultKeepId" @click="deleteVaultKeep(keep.vaultKeepId)">
                    <i class="fa fa-minus-circle text-muted" aria-hidden="true"></i>
                  </button>
                  <button class="btn" @click="deleteKeep(keep.id)" v-if="keep.creatorId == profile.id">
                    <i class="fa fa-trash-o text-muted " aria-hidden="true"></i>
                  </button>

                  <button type="button" class="close ml-2" data-dismiss="modal" aria-label="Close">
                    <i class="fa fa-times-circle-o" aria-hidden="true"></i>
                  </button>
                </div>
                <div class="d-flex justify-content-center">
                  <i class="fa fa-eye mx-2 my-auto" aria-hidden="true"></i>
                  <p class="my-auto small">
                    {{ keep.views }}
                  </p>
                  <i class="fa fa-bookmark-o ml-4 mr-2 my-auto" aria-hidden="true"></i>
                  <p class="my-auto mr-3 small">
                    {{ keep.keeps }}
                  </p>
                  <i class="fa fa-share mx-2 my-auto" aria-hidden="true"></i>
                  <p class="my-auto small">
                    {{ keep.shares }}
                  </p>
                </div>
                <h3 class="modal-title text-center">
                  {{ keep.name }}
                </h3>
                <p class="small text-center">
                  {{ keep.description }}
                </p>

                <hr class="mx-5">

                <div class="row" v-if="keep.tags">
                  <div class="col-12 d-flex justify-content-around">
                    <p v-for="tag in keep.tags.split(',')" :key="tag+keep.id" class="border radius p-2 btn-outline-dark">
                      {{ tag }}
                    </p>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12 ">
                    <router-link :to="{name: 'ActiveVault', params: {vaultId: state.vaultId}}" @click="closeModal(keep.id)" v-if="state.vaultId">
                      <div class="d-flex justify-content-center">
                        <p class="text-dark my-auto small no-decoration">
                          Go to Vault
                        </p>
                        <i class="ml-3 fa fa-arrow-right text-dark my-auto no-decoration" aria-hidden="true"></i>
                      </div>
                    </router-link>
                  </div>
                </div>
                <div class="row">
                  <div class="col-6">
                    <form action="" @submit.prevent="createVaultKeep(keep.id, state.vaultId, keep)">
                      <select v-model="state.vaultId" name="" :id="'keepVaultSelect'+keep.id" class="w-100 radius p-1" required>
                        <option value="0">
                          Select Vault
                        </option>
                        <option v-for="vault in vaults" :key="'keep'+keep.id+'vault'+vault.id" :value="vault.id" required>
                          {{ vault.name }}
                        </option>
                      </select>
                      <button class="btn btn-outline-dark btn-block radius">
                        Add to Vault
                      </button>
                    </form>
                  </div>
                  <div class="col-6 d-flex flex-column justify-content-center">
                    <router-link class="text-dark " :to="{name: 'ActiveProfile', params: {profileId: keep.creator.id}}" @click="closeModal(keep.id)">
                      <div class="d-flex justify-content-center">
                        <p class="small my-auto mr-2">
                          {{ keep.creator.email.split('@').splice(0,1).join('') }}
                        </p>
                        <img :src="keep.creator.picture" class="rounded-circle shadow" height="40" alt="">
                      </div>
                    </router-link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- </router-link> -->
  </div>
</template>

<script>

import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import router from '../router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import $ from 'jquery'
import swal from 'sweetalert'
export default {
  name: 'KeepComponent',
  props: ['keepProps'],
  setup(props) {
    const state = reactive({
      vaultId: null,
      editedKeep: AppState.activeKeep
    })
    return {
      state,
      keep: computed(() => props.keepProps),
      profile: computed(() => AppState.profile),
      activeKeep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.profile.id)),
      async setActiveKeep(keep) {
        keepsService.setActiveKeep(keep)
      },
      async getOne(id, keep) {
        keepsService.getOne(id)
        keepsService.setActiveKeep(keep)
      },
      navigateToActiveKeep(keep) {
        router.push({ name: 'ActiveKeep', params: { keepId: keep.id } })
      },
      createVaultKeep(keepId, vaultId, keep) {
        const newVaultKeep = {
          keepId: keepId,
          vaultId: vaultId
        }
        AppState.activeKeep = keep
        AppState.activeKeep.shares++
        vaultKeepsService.create(newVaultKeep)
      },
      closeModal(keepId) {
        $('#keepModal' + keepId).modal('hide')
      },
      async deleteKeep(keepId) {
        swal({
          title: 'Are you sure?',
          text: 'Once deleted, you will not be able to get your keep back',
          icon: 'warning',
          buttons: true,
          dangerMode: true
        })
          .then((willDelete) => {
            if (willDelete) {
              swal('Deleted!', {
                icon: 'success'
              })
              this.closeModal(keepId)
              keepsService.deleteKeep(keepId)
            } else {
              swal('Your keep is safe!')
            }
          })
      },
      deleteVaultKeep(vaultKeepId) {
        const vaultKeep = AppState.vaultKeeps.find(vk => vk.id === vaultKeepId)

        swal({
          title: 'Are you sure?',
          text: 'You can always store this keep in a vault later',
          icon: 'warning',
          buttons: true,
          dangerMode: true
        })
          .then((willDelete) => {
            if (willDelete) {
              swal('Deleted!', {
                icon: 'success'
              })
              if (vaultKeep) {
                this.closeModal(vaultKeep.keepId)
                vaultKeepsService.deleteVaultKeep(vaultKeepId, vaultKeep)
              }
            } else {
              swal('Your vault keep is safe!')
            }
          })
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

.card-hover:hover {
background: rgba(0, 0, 0, 0.479);
color: rgba(255, 255, 255, 0.774);
border-radius: 15px;
transition: all .3s ease-in-out;

}

.card-hover {
  color: rgba(255, 255, 255, 0);
}

button {
  z-index: 2;
}

.hoverable {
  cursor: pointer;
}

.no-decoration {
  text-decoration: none;
}
</style>
