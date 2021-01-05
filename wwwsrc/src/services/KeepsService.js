import { AppState } from '../AppState'
import { api } from '../services/AxiosService'
import { logger } from '../utils/Logger'
import { notificationService } from './NotificationService'

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
      await api.get('api/keeps/' + keepId)

      this.get()
      this.getKeepsByProfile(AppState.profile.id)
    } catch (error) {
      logger.error(error)
    }
  }

  async create(newKeep) {
    try {
      await api.post('api/keeps/', newKeep)
      this.get()
      this.getKeepsByProfile(AppState.profile.id)
      notificationService.success()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteKeep(keepId) {
    try {
      await api.delete('api/keeps/' + keepId)
      this.get()
      this.getKeepsByProfile(AppState.profile.id)
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
      logger.log(res.data)
      AppState.activeKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  setActiveKeep(keep) {
    AppState.activeKeep = keep
  }

  updateViewCount(keep) {
    keep.views++
    this.edit(keep.id, keep)
  }
}

export const keepsService = new KeepsService()
