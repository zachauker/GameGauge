<script setup>
import gameListApi from '../../api/GameListApi'
import GameDetailsCard from "@/components/Games/GameDetailsCard.vue"
import {ref} from "vue"
import {onMounted} from "vue";

const gameList = ref(null)

onMounted(() => {
  gameListApi.fetchGameListDetails(this.$route.params.id)
    .then((response) => {
      gameList.value = response.data
    })
})


</script>

<template>
<v-container :fluid="true">
  <v-row align="center" justify="center">
    <v-col cols="8" v-for="game in gameList.value.games">
      <game-details-card :game="game" ></game-details-card>
    </v-col>
  </v-row>
</v-container>
</template>

<style scoped>

</style>
