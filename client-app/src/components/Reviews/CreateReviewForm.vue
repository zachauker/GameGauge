<script>
import reviewApi from "@/api/ReviewApi"
import gameApi from  "@/api/GameApi"
export default {
  name: "CreateReviewForm",

  props: {
    gameId: {
      type: String,
      required: true
    }
  },

  data() {
    return {
      game: {},
      review: {
        gameId: this.gameId,
        score: null,
        description: null,
      },
      valid: false
    }
  },

  created() {
    gameApi.getGameDetails(this.gameId)
      .then(response => {
        this.game = response.data
      })
  },

  methods: {
    submit() {
      reviewApi.createReview(this.review)
          .then(response => {

          })
    }
  }
}
</script>

<template>
  <v-card variant="outlined" max-width="800px">
    <v-card-title>
      <v-row align="center" justify="center" class="my-2">
        <v-col cols="auto" v-if="game">
          <h3>Review {{ game.title }}</h3>
        </v-col>
      </v-row>
    </v-card-title>
    <v-card-text>
      <v-form v-model="valid" class="px-4" lazy-validation>
        <v-row align="center" justify="center" class="my-3">
          <v-col cols="auto">
            <v-rating id="score" v-model="review.score" size="large" :hover="true" :half-increments="true" :item-labels="['Hated it', '', '', '', 'Loved it']" variant="outlined"></v-rating>
          </v-col>
          <v-col cols="8">
            <v-textarea label="Description" v-model="review.description" variant="outlined"></v-textarea>
          </v-col>
        </v-row>
      </v-form>
    </v-card-text>
    <v-card-actions class="justify-center my-2">
      <v-btn color="primary" variant="outlined" @click="submit">Submit</v-btn>
    </v-card-actions>
  </v-card>

</template>

<style scoped>

</style>
