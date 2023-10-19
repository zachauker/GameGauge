// Utilities
import { defineStore } from 'pinia'
import gameApi from "@/api/GameApi"


export const useSearchStore = defineStore('search', {
  state: () => ({
    results: null
  }),

  actions: {
    async searchGames(query) {
      try {
        this.results = await gameApi.searchGames(query)
      }
      catch (error) {
        console.log(error)
      }
    },
  },

  getters: {
    getResults(state) {
      return state.results
    }
  }
})
