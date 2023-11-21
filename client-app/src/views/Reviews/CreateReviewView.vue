<script setup>
import CreateReviewForm from "@/components/Reviews/CreateReviewForm.vue"
</script>

<template>
  <v-container :fluid="true" class="fill-height">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="4">
        <create-review-form :game-id="this.$route.params.id"/>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
