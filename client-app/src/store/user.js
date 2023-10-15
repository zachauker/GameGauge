// Utilities
import { defineStore } from 'pinia'

import userApi from "../api/UserApi"

export const useUserStore = defineStore('user', {
  state: () => ({
    user: null
  }),

  actions: {
    async loginUser(loginData) {
      try {
        this.user = await userApi.loginUser(loginData)
      }
      catch (error) {
        console.log(error)
      }
    },
  },

  getters: {
    getUser(state) {
      return state.user
    }
  }
})
