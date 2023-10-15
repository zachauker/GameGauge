<script>
import { fetchGameListDetails } from '../../api/GameListApi'
export default {
  name: "GameListDetails",
  data() {
    return {
      gameList: null
    }
  },

  created() {
    fetchGameListDetails(this.$route.params.id)
      .then((response) => {
        this.gameList = response.data
      })
  }
}
</script>

<template>

</template>

<style scoped>

</style>
