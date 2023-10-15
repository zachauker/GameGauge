<script>
import { createGameList } from '../../api/GameListApi'
import { useRouter } from "vue-router"

const router = useRouter()

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
      createGameList(this.gameList)
          .then(response => {
            router.push("/gamelists/" + response.data.id + "/addgames")
          })
    }
  }
}
</script>

<template>
  <v-card variant="outlined" max-width="800px">
    <v-card-text>
      <v-responsive :aspect-ratio="4/3">
        <v-form v-model="valid" class="px-4" lazy-validation>
          <v-row align="center" justify="center" class="my-3">
            <v-col cols="8">
              <v-text-field label="List Title" v-model="gameList.title" variant="outlined"></v-text-field>
              <v-textarea label="Description" v-model="gameList.description" variant="outlined"></v-textarea>
            </v-col>
          </v-row>
        </v-form>
      </v-responsive>
    </v-card-text>
    <v-card-actions class="justify-center my-2">
      <v-btn color="primary" variant="outlined" @click="submit">Submit</v-btn>
    </v-card-actions>
  </v-card>

</template>

<style scoped>

</style>
