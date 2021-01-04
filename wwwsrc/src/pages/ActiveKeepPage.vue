<template>
  <div class="active-keep-page">
    <div class="row pt-5 px-4">
      <div class="col-12 d-flex justify-content-end">
        <button class="btn">
          <i class="fa fa-ellipsis-v" aria-hidden="true"></i>
        </button>

        <button class="btn" @click="deleteKeep(keep.id)">
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
              <select v-model="state.vaultId" name="" id="">
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
import router from '../router'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'ActiveKeepPage',
  setup() {
    const state = reactive({
      vaultId: null
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
        router.push({ name: 'Home' })
        keepsService.deleteKeep(keepId)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
