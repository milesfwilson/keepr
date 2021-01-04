import { AppState } from '../AppState'
import { api } from '../services/AxiosService'
import { logger } from '../utils/Logger'
import { keepsService } from './KeepsService'

class VaultKeepsService {
  async get() {
    try {
      const res = await api.get('api/vaultkeeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getOne(vaultKeepId) {
    try {
      const res = await api.get('api/vaultkeeps/' + vaultKeepId)
      AppState.activeVaultKeep = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async create(newVaultKeep) {
    try {
      await api.post('api/vaultkeeps/', newVaultKeep)
      this.get()
      AppState.activeKeep.keeps++
      keepsService.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVaultKeep(vaultKeepId) {
    try {
      await api.delete('api/vaultkeeps/' + vaultKeepId)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(vaultKeepId, editedVaultKeep) {
    try {
      await api.put('api/vaultkeeps/' + vaultKeepId, editedVaultKeep)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
