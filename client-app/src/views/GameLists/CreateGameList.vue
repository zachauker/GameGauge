<script>
import CreateGameListForm from "../../components/GameLists/CreateGameListForm.vue"

export default {
  name: "CreateGameList",
  components: {CreateGameListForm}
}
</script>

<template>
  <v-container :fluid="true" class="fill-height">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="4">
        <v-card variant="outlined" max-width="600px">
          <v-card-text>
            <v-responsive :aspect-ratio="1">
              <create-game-list-form></create-game-list-form>
            </v-responsive>
          </v-card-text>
          <v-card-actions class="justify-center">
            <v-btn color="primary" variant="outlined" @click="submitForm">Submit</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
