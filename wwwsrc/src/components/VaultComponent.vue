<template>
  <div class="vault-component">
    <router-link :to="{name: 'ActiveVault', params: {vaultId: vault.id}}" class="text-dark radius vault" @click="setActiveVault(vault)">
      <div class="m-2 p-3 shadow d-flex radius vault">
        <h5 class="text-center my-auto">
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
.radius {
  border-radius: 25px;
  text-decoration: none;
}

.vault:hover {
  background-color: rgb(43, 43, 43);
  transition: all .5s ease-in-out;
color: white;
}
.vault {
background-color: white;
}
</style>
