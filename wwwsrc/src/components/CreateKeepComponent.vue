<template>
  <div class="create-keep-component">
    <button class="btn mx-3" data-toggle="modal" data-target="#createKeepModal">
      <i class="fa fa-plus-circle fa-2x" aria-hidden="true"></i>
    </button>

    <!-- Modal -->
    <div class="modal fade"
         id="createKeepModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Create Keep
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form action="" @submit.prevent="create(state.newKeep)">
              <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.newKeep.name" placeholder="Keep Name" required>
              <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.newKeep.description" placeholder="Keep Description" required>
              <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.newKeep.img" placeholder="Keep Image URL" required>
              <input class=" p-2 my-1 form-control w-100" type="text" v-model="state.newKeep.tags" placeholder="Enter tags (separated with a comma)">
              <button class="btn btn-outline-dark btn-block">
                Create
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  name: 'CreateKeepComponent',
  setup() {
    const state = reactive({
      newKeep: {
        name: null,
        description: null,
        img: null,
        tags: null
      }
    })
    return {
      state,
      create(newKeep) {
        keepsService.create(newKeep)
        keepsService.getKeepsByProfile(AppState.profile.id)
        $('#createKeepModal').modal('hide')
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
