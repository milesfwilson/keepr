import { initialize } from '@bcwdev/auth0provider-client'
import { AppState } from '../AppState'
import { audience, clientId, domain } from '../AuthConfig'
import router from '../router'
import { setBearer } from './AxiosService'
import { keepsService } from './KeepsService'
import { profileService } from './ProfileService'
import { vaultKeepsService } from './VaultKeepsService'
import { vaultsService } from './VaultsService'

export const AuthService = initialize({
  domain,
  clientId,
  audience,
  onRedirectCallback: appState => {
    router.push(
      appState && appState.targetUrl
        ? appState.targetUrl
        : window.location.pathname
    )
  }
})

AuthService.on(AuthService.AUTH_EVENTS.AUTHENTICATED, async function() {
  setBearer(AuthService.bearer)
  await profileService.getProfile()
  AppState.user = AuthService.user
  await keepsService.get()
  await vaultsService.get()
  await vaultKeepsService.get()

  // NOTE if there is something you want to do once the user is authenticated, place that here
})
