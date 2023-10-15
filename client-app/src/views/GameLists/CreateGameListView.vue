<script setup>
import CreateGameListForm from "../../components/GameLists/CreateGameListForm.vue"

</script>

<template>
  <v-container :fluid="true" class="fill-height">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="4">
        <create-game-list-form></create-game-list-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
