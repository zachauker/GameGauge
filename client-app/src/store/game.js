// Utilities
import {defineStore} from 'pinia'
import gameApi from "@/api/GameApi"
import { useRoute } from "vue-router";

export const useGameStore = defineStore('game', () =>{
  const route = useRoute()
  return {
    state: () => ({
      game: null
    }),

    actions: {
      async getGameDetails(gameId) {
        try {
          this.game = await gameApi.getGameDetails(gameId)
        } catch (error) {
          console.log(error)
        }
      },
    },

    getters: {
      getUser(state) {
        return state.user
      }
    }
  }
})
