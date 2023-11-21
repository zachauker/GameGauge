<script setup>
import LoginForm from "../../components/Accounts/LoginForm.vue"
</script>

<template>
  <v-container :fluid="true" class="fill-height">
    <v-row align="center" justify="center">
      <v-col cols="12" lg="4" sm="8" md="4">
        <login-form></login-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
