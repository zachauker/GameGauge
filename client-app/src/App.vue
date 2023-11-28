<template>
  <router-view/>
</template>

<script setup>
import {getToken, generateToken} from '../src/api/TokenStorage'
import {ref, onMounted} from 'vue'

onMounted(() => {
  console.log(getToken())
  if (!getToken()) {
    generateToken()
  }
})

</script>
