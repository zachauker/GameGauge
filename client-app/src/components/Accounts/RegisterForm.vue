<script setup>
import userApi from "@/api/UserApi"
import { useRouter } from "vue-router"

const router = useRouter()

import {ref} from "vue";

const tempUser = ref({
  "email": null,
  "password": null,
  "username": null,
  "displayname": null
})

const valid = ref(false)
const visible = ref(false)

function register() {
  userApi.registerUser(tempUser.value)
    .then(() => {
      router.push("/login")
    })
}

</script>

<template>
  <v-card variant="outlined" max-width="800px">
    <v-card-text>
      <v-responsive :aspect-ratio="4/3">
        <v-form v-model="valid" class="px-4" lazy-validation>
          <v-row align="center" justify="center" class="my-3">
            <v-col cols="8">
              <div class="text-subtitle-1 text-medium-emphasis">Account</div>
              <v-text-field
                placeholder="Email address"
                prepend-inner-icon="mdi-email-outline"
                variant="outlined"
                v-model="tempUser.email"
              ></v-text-field>

              <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between">
                Password
              </div>
              <v-text-field
                :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
                :type="visible ? 'text' : 'password'"
                placeholder="Enter your password"
                prepend-inner-icon="mdi-lock-outline"
                variant="outlined"
                @click:append-inner="visible = !visible"
                v-model="tempUser.password"
              ></v-text-field>

              <div class="text-subtitle-1 text-medium-emphasis">Username</div>
              <v-text-field
                placeholder="Username"
                prepend-inner-icon="mdi-badge-account-outline"
                variant="outlined"
                v-model="tempUser.username"
              ></v-text-field>

              <div class="text-subtitle-1 text-medium-emphasis">Display Name</div>
              <v-text-field
                placeholder="Display Name"
                prepend-inner-icon="mdi-account-details-outline"
                variant="outlined"
                v-model="tempUser.displayname"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-form>
      </v-responsive>
    </v-card-text>
    <v-card-actions class="justify-center my-2">
      <v-btn color="primary" variant="outlined" @click="register">Create Account</v-btn>
    </v-card-actions>
  </v-card>
</template>

<style scoped>

</style>
