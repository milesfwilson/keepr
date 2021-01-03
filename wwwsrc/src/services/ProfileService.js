import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('api/profiles')
      AppState.profile = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getProfileById(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId)
      AppState.activeProfile = res.data
    } catch (error) {
      logger.error(error)
    }
  }
}

export const profileService = new ProfileService()
