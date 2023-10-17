<script>
import gameListApi from "@/api/GameListApi"

export default {
  Name: "GameListDetailsCard",
  props: {
    gameList: {
      type: Object,
      required: true
    },

    data() {
      return {
        loading: true
      }
    },

    created() {
      gameListApi.fetchGameListDetails(this.gameList)
          .then(response => {
            this.gameList = response.data
            this.loading = false
          })
    }
  }
}
</script>

<template>
  <v-card variant="outlined" v-if="gameList">
    <v-card-title>{{ gameList.title }}</v-card-title>
    <v-card-text>
      {{ gameList.description }}
    </v-card-text>
  </v-card>
</template>

<style scoped>

</style>
