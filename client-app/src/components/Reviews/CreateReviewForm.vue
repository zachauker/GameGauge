<script>
import reviewApi from "@/api/ReviewApi"
export default {
  name: "CreateReviewForm",

  props: {
    game: {
      type: Object,

    }
  },

  data() {
    return {
      review: {
        score: null,
        description: null,
      },
      valid: false
    }
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
        <v-col cols="auto">
          <h3>Review {{ game.title }}</h3>
        </v-col>
      </v-row>
    </v-card-title>
    <v-card-text>
      <v-form v-model="valid" class="px-4" lazy-validation>
        <v-row align="center" justify="center" class="my-3">
          <v-col cols="8">
            <v-rating label="Score" v-model="review.score" variant="outlined"></v-rating>
            <v-textarea label="Reveiw" v-model="review.description" variant="outlined"></v-textarea>
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
