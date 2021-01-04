import { AppState } from '../AppState'
import { api } from '../services/AxiosService'
import { logger } from '../utils/Logger'

class KeepsService {
  async get() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getOne(keepId) {
    try {
      const res = await api.get('api/keeps/' + keepId)
      AppState.activeKeep = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async create(newKeep) {
    try {
      await api.post('api/keeps/', newKeep)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteKeep(keepId) {
    try {
      await api.delete('api/keeps/' + keepId)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(keepId, editedKeep) {
    try {
      await api.put('api/keeps/' + keepId, editedKeep)
      this.get()
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      AppState.profileKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByVault(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.activeKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  setActiveKeep(keep) {
    AppState.activeKeep = keep
  }
}

export const keepsService = new KeepsService()
