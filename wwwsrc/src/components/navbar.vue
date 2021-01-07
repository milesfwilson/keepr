<template>
  <nav class="navbar navbar-expand-lg navbar-dark nav-gradient">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <h3><i class="fa fa-bookmark fa mr-1"></i> k e e p e r</h3>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav mr-auto">
        <!-- <li class="nav-item">
          <router-link :to="{ name: 'Home' }" class="nav-link">
            Home
          </router-link>
        </li>
        <li class="nav-item">
          <router-link :to="{ name: 'About' }" class="nav-link">
            About
          </router-link>
        </li> -->
      </ul>
      <span class="navbar-text">
        <button
          class="btn"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          <i class="fa fa-user-circle-o fa-2x text-light" aria-hidden="true"></i>
        </button>

        <div class="dropdown" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="50"
              class="rounded-circle"
            />
            {{ user.name.split('@').splice(0,1).join('') }}
          </div>
          <div
            class="dropdown-menu list-group radius"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <router-link :to="{ name: 'ActiveProfile', params: {profileId: profile.id}}">
              <div class="list-group-item list-group-item-action hoverable border-0 bg-transparent">
                Profile
              </div>
            </router-link>
            <div
              class="list-group-item list-group-item-action hoverable border-0 bg-transparent"
              @click="logout"
            >
              Logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  name: 'Navbar',
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      user: computed(() => AppState.user),
      profile: computed(() => AppState.profile),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}

.list-group  {

}
.dropdown-menu.show {

  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--light);
}

</style>
