<script setup>
import { ref } from "vue"
import SearchGames from "@/components/Games/SearchGames.vue"
import GameDetailsCard from "@/components/Games/GameDetailsCard.vue"
import { useSearchStore } from "@/store/search"
import GameListDetailsCard from "@/components/GameLists/GameListDetailsCard.vue"

const searchStore = useSearchStore()

</script>

<template>
  <v-container :fluid="true">
<!--    <v-row align="center" justify="center">-->
<!--      <v-col col="8">-->
<!--        <game-list-details-card></game-list-details-card>-->
<!--      </v-col>-->
<!--    </v-row>-->
    <v-row align="center" justify="center">
      <v-col cols="8">
        <search-games></search-games>
      </v-col>
    </v-row>
    <v-row align="start" justify="space-around" v-if="searchStore.results">
      <v-col cols="12" md="4" sm="6" v-for="result in searchStore.results.items" :key="result.id" style="height: 300px">
        <game-details-card :game="result" :showAdd="true"></game-details-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
