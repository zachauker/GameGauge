<script setup>
import {ref, computed, defineProps, onMounted} from 'vue'
import gameApi from "@/api/GameApi"

const props = defineProps({
  game: Object,
})

const loading = ref(true)
const screenshots = ref(null)
onMounted(() => {
  gameApi.getGameScreenshots(props.game.id)
    .then(response => {
      screenshots.value = response.data
      loading.value = false
    })
})

const coverImageUrl = computed(() => {
  return 'https://images.igdb.com/igdb/image/upload/t_cover_big/' + props.game.covers[0].imageId + '.jpg'
})

const bannerImageUrl = computed(() => {
  return 'https://images.igdb.com/igdb/image/upload/t_screenshot_big/' + screenshots.value[0].imageId + '.jpg'
})

function formatTimestamp(timestamp) {
  const options = {year: 'numeric', month: 'long', day: 'numeric'};
  const date = new Date(timestamp);
  return date.toLocaleDateString('en-US', options);
}

</script>

<template>
  <div class="banner-container" v-if="!loading">
      <v-parallax v-if="screenshots" :src="bannerImageUrl" class="banner-image">
      </v-parallax>
      <v-img v-if="coverImageUrl" :src="coverImageUrl" alt="" class="cover-image" :height="352" :width="264"></v-img>
      <div class="game-details-wrapper">
        <div class="text-h2 text-left mb-5">
          {{ game.title }}
        </div>
        <div class="text-h4 text-left">
          {{ formatTimestamp(game.releaseDate) }}
        </div>
        <div class="text-h4 text-left">
          {{ game.in }}
        </div>
      </div>
  </div>
</template>

<style scoped>
.banner-container {
  position: relative;
  width: 100%;
  height: 450px; /* Adjust the height as needed */
}

.banner-image {
  width: 100%;
  height: 450px; /* Adjust the height as needed */
  position: relative;
  overflow: hidden;
  background-size: cover;
  background-position: center center;
  filter: blur(5px);
  transform: scale(1.1);
}

.banner-image::before {
  content: "";
  position: absolute;
  width: 100%;
  height: 100%;
  filter: blur(8px); /* Adjust the blur amount as needed */
}

/* Smaller Overlapping Image Styles */
.cover-image {
  position: absolute;
  top: 85%; /* Adjust vertical position as needed */
  left: 35%; /* Adjust horizontal position as needed */
  transform: translate(-50%, -50%);
  width: 100px; /* Adjust the width as needed */
  height: 100px; /* Adjust the height as needed */
  z-index: 2; /* Ensure the smaller image appears above the banner */
}

/* Text Content Styles */
.game-details-wrapper {
  position: absolute;
  top: 65%; /* Adjust vertical position as needed */
  left: 50%; /* Adjust horizontal position as needed */
  transform: translate(-50%, -50%);
  z-index: 3; /* Ensure the text content appears above the overlay and thumbnail */
  color: white; /* Text color */
}
</style>
