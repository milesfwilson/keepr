import { AppState } from '../AppState'
import { api } from '../services/AxiosService'
import { logger } from '../utils/Logger'

class VaultKeepsService {
  async get() {
    try {
      const res = await api.get('api/vaultKeeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getOne(vaultKeepId) {
    try {
      const res = await api.get('api/vaultKeeps/' + vaultKeepId)
      AppState.activeVaultKeep = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async create(newVaultKeep) {
    try {
      await api.post('api/vaultKeeps/', newVaultKeep)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVaultKeep(vaultKeepId) {
    try {
      await api.delete('api/vaultKeeps/' + vaultKeepId)
      const index = AppState.vaultKeeps.findIndex(k => k.id === vaultKeepId)
      AppState.vaultKeeps.splice(index, 1)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(vaultKeepId, editedVaultKeep) {
    try {
      await api.put('api/vaultKeeps/' + vaultKeepId, editedVaultKeep)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
