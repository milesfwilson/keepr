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

  async create(newVault) {
    try {
      await api.post('api/vaults/', newVault)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVault(vaultId) {
    try {
      await api.delete('api/vaults/' + vaultId)
      const index = AppState.vaults.findIndex(k => k.id === vaultId)
      AppState.vaults.splice(index, 1)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(vaultId, editedVault) {
    try {
      await api.put('api/vaults/' + vaultId, editedVault)
      this.get()
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
}

export const vaultsService = new VaultsService()
