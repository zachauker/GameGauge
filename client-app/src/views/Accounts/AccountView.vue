<script setup>
import {useUserStore} from "@/store/user"

const userStore = useUserStore()
</script>

<template>
  <v-container :fluid="true" class="mt-5">
    <v-row align="center" justify="center">
      <v-col cols="4">
        <v-card variant="outlined">
          <v-card-title>
            Account Details
          </v-card-title>
          <v-card-text>
            <v-list>
              <v-list-item title="Username: " :subtitle="userStore.user.userName">
              </v-list-item>
              <v-list-item title="Display Name: " :subtitle="userStore.user.displayName">
              </v-list-item>
              <v-list-item title="Email: " :subtitle="userStore.user.email">
              </v-list-item>
            </v-list>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>

</style>
