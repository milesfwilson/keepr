<template>
  <div class="vault-component">
    <router-link :to="{name: 'ActiveVault', params: {vaultId: vault.id}}" class="text-dark" @click="setActiveVault(vault)">
      <div class="m-2 p-3 shadow rounded border bg-light d-flex">
        <h5 class="text-center text-dark my-auto">
          {{ vault.name }}
        </h5>
        <i v-if="vault.isPrivate" class="fa fa-eye-slash my-auto px-1" aria-hidden="true"></i>
      </div>
    </router-link>
  </div>
</template>

<script>
import { computed } from 'vue'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultComponent',
  props: ['vaultProps'],
  setup(props) {
    return {
      vault: computed(() => props.vaultProps),
      setActiveVault(id) {
        vaultsService.setActiveVault(id)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
