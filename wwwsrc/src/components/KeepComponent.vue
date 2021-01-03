<template>
  <div class="keep-component card radius bg-transparent border-0 shadow" @click="setActiveKeep(keep)" data-toggle="modal" :data-target="'#keepModal'+keep.id">
    <img class="card-img-top radius" :src="keep.img" alt="Card image">
    <div class="card-img-overlay d-flex flex-column justify-content-end">
      <div class="d-flex justify-content-between">
        <h4 class="card-title my-auto">
          {{ keep.name }}
        </h4>

        <img :src="keep.creator.picture" class="rounded-circle shadow" height="40">
      </div>
    </div>

    <!-- Modal -->
    <div class="modal fade"
         :id="'keepModal'+keep.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Modal title
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <div class="row">
                <div class="col-6">
                  <img :src="keep.img" alt="">
                </div>
                <div class="col-6">
                  <div class="row">
                    <div class="col-12">
                      <h3>
                        {{ keep.name }}
                      </h3>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-12">
                      <h3>
                        {{ keep.description }}
                      </h3>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-12">
                      <p>
                        {{ keep.shares }}
                        {{ keep.keeps }}
                        {{ keep.views }}
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button type="button" class="btn btn-primary">
              Save
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'KeepComponent',
  props: ['keepProps'],
  setup(props) {
    return {
      keep: computed(() => props.keepProps),
      setActiveKeep(keep) {
        keepsService.setActiveKeep(keep)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
