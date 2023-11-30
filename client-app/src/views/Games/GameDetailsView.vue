<script setup>
import {ref, onMounted} from "vue"
import GameDetailsHeader from "@/components/Games/GameDetailsHeader.vue"
import gameApi from "@/api/GameApi"
import {useRoute} from "vue-router"

const route = useRoute()
const game = ref(null)

onMounted(() => {
  gameApi.getGameDetails(route.params.id)
    .then(response => {
      game.value = response.data
    })
})

</script>

<template>
  <div v-if="game">
    <game-details-header :game="game"/>
    <v-container :fluid="true">
      <v-row align="center" justify="center" v-if="game">
        <v-col cols="12">
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<style scoped>

</style>
