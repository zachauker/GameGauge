<script>
import gameApi from "@/api/GameApi"
export default {
  Name: "GameDetailsCard",
  props: {
    game: {
      type: Object,
      required: true
    },

    showAdd: {
      type: Boolean,
      defaultValue: false
    }
  },

  methods: {
    addGameToList(gameId) {
      gameApi.addGameToList()
    }
  }
}

</script>

<template>
  <v-card variant="outlined" class="fill-height">
    <v-img src=""></v-img>
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
          <v-btn variant="outlined" class="float-end">View</v-btn>
        </v-col>
        <v-col v-if="showAdd" cols="auto">
          <v-btn variant="outlined" @click="addGameToList(game.id)">Add</v-btn>
        </v-col>
      </v-row>
    </v-card-actions>
  </v-card>
</template>

<style scoped>

</style>
