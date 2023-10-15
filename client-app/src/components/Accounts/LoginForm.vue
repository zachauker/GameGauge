<script setup>
import { useUserStore } from "@/store/user"
const userStore = useUserStore()

import {ref} from "vue";

const tempUser = ref({
  "email": null,
  "password": null
})

function login() {
  userStore.loginUser(tempUser.value)
}

</script>

<template>
  <v-card variant="outlined" max-width="800px">
    <v-card-text>
      <v-responsive :aspect-ratio="4/3">
        <v-form v-model="valid" class="px-4" lazy-validation>
          <v-row align="center" justify="center" class="my-3">
            <v-col cols="8">
              <v-text-field label="Email" v-model="tempUser.email" variant="outlined"></v-text-field>
              <v-text-field label="Description" v-model="tempUser.password" variant="outlined"></v-text-field>
            </v-col>
          </v-row>
        </v-form>
      </v-responsive>
    </v-card-text>
    <v-card-actions class="justify-center my-2">
      <v-btn color="primary" variant="outlined" @click="login">Login</v-btn>
    </v-card-actions>
  </v-card>
</template>

<style scoped>

</style>
