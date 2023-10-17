<script>
import gameListApi from '@/api/GameListApi'

export default {
  name: "CreateGameListForm",
  data() {
    return {
      gameList: {
        title: null,
        description: null,
      },
      valid: false
    }
  },

  methods: {
    submit() {
      gameListApi.createGameList(this.gameList)
          .then(response => {
            this.$router.push("/gamelists/" + response.data.id + "/add")
          })
    }
  }
}
</script>

<template>
  <v-card variant="outlined" max-width="800px">
    <v-card-title>
      <v-row align="center" justify="center" class="my-2">
        <v-col cols="auto">
          <h3>Create List</h3>
        </v-col>
      </v-row>
    </v-card-title>
    <v-card-text>
      <v-form v-model="valid" class="px-4" lazy-validation>
        <v-row align="center" justify="center" class="my-3">
          <v-col cols="8">
            <v-text-field label="List Title" v-model="gameList.title" variant="outlined"></v-text-field>
            <v-textarea label="Description" v-model="gameList.description" variant="outlined"></v-textarea>
          </v-col>
        </v-row>
      </v-form>
    </v-card-text>
    <v-card-actions class="justify-center my-2">
      <v-btn color="primary" variant="outlined" @click="submit">Submit</v-btn>
    </v-card-actions>
  </v-card>

</template>

<style scoped>

</style>
