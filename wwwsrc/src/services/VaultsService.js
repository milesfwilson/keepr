import { AppState } from '../AppState'
import { api } from '../services/AxiosService'
import { logger } from '../utils/Logger'

class VaultsService {
  async get() {
    try {
      const res = await api.get('api/vaults')
      AppState.vaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getOne(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async create(newVault, profileId) {
    try {
      await api.post('api/vaults/', newVault)
      this.get()
      this.getVaultsByProfile(profileId)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVault(vaultId, profileId) {
    try {
      await api.delete('api/vaults/' + vaultId)
      this.get()
      this.getVaultsByProfile(profileId)
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(vaultId, editedVault) {
    try {
      await api.put('api/vaults/' + vaultId, editedVault)
      AppState.activeVault = editedVault
      this.get()
      this.getVaultsByProfile(editedVault.creatorId)
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultsByProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      AppState.profileVaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  setActiveVault(vault) {
    AppState.activeVault = vault
    logger.log(vault)
  }
}

export const vaultsService = new VaultsService()
