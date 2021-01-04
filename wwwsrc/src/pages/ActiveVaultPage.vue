<template>
  <div class="active-vault-page">
    <div class="row">
      <div class="col-12 text-center">
        <h3>
          {{ activeVault.name }}
        </h3>
        <h3>
          {{ activeVault.description }}
        </h3>
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
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { computed, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
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
      activeKeeps: computed(() => AppState.activeKeeps)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
