<script>
import { fetchGameListDetails } from '../../store/api/GameListApi'
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
<v-container>
  
</v-container>
</template>

<style scoped>

</style>
