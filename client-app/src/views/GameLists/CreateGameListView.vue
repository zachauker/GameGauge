<script>
import CreateGameListForm from "../../components/GameLists/CreateGameListForm.vue"

export default {
  name: "CreateGameList",
  components: {CreateGameListForm},

  methods: {
    redirectToList(gameList) {
      this.$router.push('/gamelists/' + gameList.id)
    }
  }
}
</script>

<template>
  <v-container :fluid="true" class="fill-height">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="4">
        <create-game-list-form @game-list-created="redirectToList"></create-game-list-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
