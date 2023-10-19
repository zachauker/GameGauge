<script setup>
import {useRouter} from "vue-router";

const router = useRouter()
const props = defineProps({
  game: {
    type: Object,
    required: true
  },

  showAdd: {
    type: Boolean,
    defaultValue: false
  }
})

function viewGame() {
  router.push("/games/" + props.game.id)
}
</script>

<template>
  <v-card variant="outlined" class="fill-height">
    <v-card-title>{{ game.title }}</v-card-title>
    <v-card-subtitle>
      {{ game.rating }}
    </v-card-subtitle>
    <v-card-text class="content">
      {{ game.description }}
    </v-card-text>
    <v-card-actions>
      <v-row align="center" justify="end">
        <v-col cols="auto">
          <v-btn variant="outlined" class="float-end" @click="viewGame">View</v-btn>
        </v-col>
        <v-col v-if="showAdd" cols="auto">
          <v-btn variant="outlined" @click="$emit('add-game', game)">Add</v-btn>
        </v-col>
      </v-row>
    </v-card-actions>
  </v-card>
</template>

<style scoped>

</style>
