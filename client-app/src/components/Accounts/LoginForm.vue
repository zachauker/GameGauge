<script setup>
import {useUserStore} from "@/store/user"

const userStore = useUserStore()
import {useRouter} from "vue-router"

const router = useRouter()

import {ref} from "vue";

const tempUser = ref({
  "email": null,
  "password": null
})

const valid = ref(false)
const visible = ref(false)

function login() {
  userStore.loginUser(tempUser.value)
      .then(() => {
        router.push("/account")
      })
}

</script>

<template>
  <v-card variant="outlined" max-width="800px">
    <v-card-title>
      <v-row align="center" justify="center" class="my-2">
        <v-col cols="auto">
          <h3>User Login</h3>
        </v-col>
      </v-row>
    </v-card-title>
    <v-card-text>
      <v-form v-model="valid" class="px-4" lazy-validation>
        <v-row align="center" justify="center" class="my-3">
          <v-col cols="8">
            <v-text-field label="Email" v-model="tempUser.email" variant="outlined"
                          prepend-inner-icon="mdi-email-outline"></v-text-field>
            <v-text-field label="Password" v-model="tempUser.password" variant="outlined"
                          :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
                          :type="visible ? 'text' : 'password'"
                          @click:append-inner="visible = !visible"
                          prepend-inner-icon="mdi-lock-outline"></v-text-field>
          </v-col>
        </v-row>
      </v-form>
    </v-card-text>
    <v-card-actions class="justify-center mb-2">
      <v-btn color="primary" variant="outlined" @click="login">Login</v-btn>
    </v-card-actions>
  </v-card>
</template>

<style scoped>

</style>
