<script setup>
import {ref } from "vue"
import { onMounted } from "vue";
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
  <v-container :fluid="true">
    <v-row align="center" justify="center" v-if="game">
      <v-col cols="12">
        {{ game.title }}
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
