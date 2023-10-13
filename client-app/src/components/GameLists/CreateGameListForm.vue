<script>
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
  }
}
</script>

<template>
  <v-form v-model="valid" class="px-4" lazy-validation>
    <v-row align="center" justify="center" class="my-3">
      <v-col cols="8">
        <v-text-field label="List Title" v-model="gameList.title" variant="outlined"></v-text-field>
        <v-textarea label="Description" v-model="gameList.description" variant="outlined"></v-textarea>
      </v-col>
    </v-row>
  </v-form>
</template>

<style scoped>

</style>
