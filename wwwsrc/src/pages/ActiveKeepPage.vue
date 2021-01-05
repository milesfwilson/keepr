<template>
  <div class="active-keep-page">
    <div class="row pt-5 px-4">
      <div class="col-12 d-flex justify-content-end">
        <!-- Button trigger modal -->
        <button v-if="keep.vaultKeepId || keep.creatorId == profile.id" type="button" class="btn" data-toggle="modal" data-target="#activeKeepModal">
          <i class="fa fa-ellipsis-v" aria-hidden="true"></i>
        </button>

        <!-- Modal -->
        <div class="modal fade"
             id="activeKeepModal"
             tabindex="-1"
             role="dialog"
             aria-labelledby="modelTitleId"
             aria-hidden="true"
        >
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">
                  Edit
                </h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form action="" @submit.prevent="editKeep(keep.id, state.editedKeep)" v-if="keep.creatorId == profile.id">
                  <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.editedKeep.name" placeholder="Keep Name" required>
                  <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.editedKeep.description" placeholder="Keep Description" required>
                  <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.editedKeep.img" placeholder="Keep Image URL" required>
                  <input class="p-2 my-1 form-control w-100" type="text" v-model="state.editedKeep.tags" placeholder="Enter tags (separated with a comma)">
                  <button class="btn btn-outline-dark btn-block">
                    Save
                  </button>
                </form>

                <button class="btn btn-block btn-danger radius" v-if="keep.vaultKeepId" @click="deleteVaultKeep(keep.vaultKeepId)">
                  Remove from Vault
                </button>
              </div>
            </div>
          </div>
        </div>

        <button class="btn" @click="deleteKeep(keep.id)" v-if="keep.creatorId == profile.id">
          <i class="fa fa-trash-o text-muted" aria-hidden="true"></i>
        </button>
      </div>
    </div>
    <div class="row px-5">
      <div class="col-md-6 col-12 d-flex flex-column justify-content-center">
        <!-- <div :style="'background-image: url('+keep.img+')'" class="h-100 modal-img">
        </div> -->
        <img :src="keep.img" class="img-fluid" alt="">
      </div>
      <div class="col-md-6 col-12 d-flex flex-column justify-content-between bg-light p-2">
        <div class="row pt-2">
          <div class="col-12">
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
          </div>
        </div>
        <div class="row my-3">
          <div class="col-12">
            <h2 class="text-center">
              {{ keep.name }}
            </h2>
          </div>
        </div>
        <div class="row my-3">
          <div class="col-12">
            <p class="text-center">
              {{ keep.description }}
            </p>
          </div>
        </div>
        <div class="row">
          <div class="col-8 offset-2">
            <hr>
          </div>
        </div>
        <div class="row my-3" v-if="keep.tags">
          <div class="col-12 d-flex justify-content-around">
            <p v-for="tag in keep.tags.split(',')" :key="tag+keep.id" class="border rounded p-1 btn-outline-dark">
              {{ tag }}
            </p>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-12 d-flex justify-content-center">
            <form action="" @submit.prevent="createVaultKeep(keep.id, state.vaultId)">
              <select v-model="state.vaultId" name="" id="" required>
                <option>Select a vault</option>
                <option v-for="vault in vaults" :key="'vault'+vault.id" :value="vault.id" required>
                  {{ vault.name }}
                </option>
              </select>
              <button class="btn btn-outline-dark btn-block">
                Add to Vault
              </button>
            </form>
          </div>
          <div class="col-lg-6 col-12 d-flex flex-column justify-content-end">
            <router-link class="text-dark " :to="{name: 'ActiveProfile', params: {profileId: keep.creator.id}}">
              <div class="d-flex justify-content-end">
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
</template>

<script>
import { useRoute } from 'vue-router'
import router from '../router'
import { AppState } from '../AppState'
import { computed, reactive, onMounted } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import swal from 'sweetalert'

export default {
  name: 'ActiveKeepPage',
  setup() {
    const state = reactive({
      vaultId: null,
      editedKeep: AppState.activeKeep
    })
    const route = useRoute()
    onMounted(() => {
      keepsService.getOne(route.params.keepId)
    })
    return {
      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.profile.id)),
      createVaultKeep(keepId, vaultId) {
        const newVaultKeep = {
          keepId: keepId,
          vaultId: vaultId
        }

        vaultKeepsService.create(newVaultKeep)
      },
      state,
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
              router.push({ name: 'Home' })
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

              $('#activeKeepModal').modal('hide')
              if (vaultKeep) {
                keepsService.getKeepsByVault(vaultKeep.vaultId)
                router.push({ name: 'ActiveVault', params: { vaultId: vaultKeep.vaultId } })
              }
              vaultKeepsService.deleteVaultKeep(vaultKeepId)
            } else {
              swal('Your vault keep is safe!')
              keepsService.getKeepsByVault(vaultKeep.vaultId)
            }
          })
      },
      async editKeep(id, editedKeep) {
        keepsService.edit(id, editedKeep)
      }
      // async keepCount(keepId) {
      //   let keepCountNum = 0
      //   for (let i = 0; i < AppState.vaultKeeps.length; i++) {
      //     if (AppState.vaultKeeps[i] === keepId) {
      //       console.log(keepCountNum++)
      //     }
      //   }
      //   return keepCountNum
      // }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
