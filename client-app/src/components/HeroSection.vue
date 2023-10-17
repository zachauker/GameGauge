<script>
export default {
  name: "HeroSection"
}
</script>

<template>
  <section class="hero is-primary">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">

        </h1>
        <h2 class="subtitle">

        </h2>
      </div>

    </div>
  </section>
</template>

<style scoped>
// Hero component styles
.hero {
  background-color: $secondary;
  color: white;
  position: relative;
}

.hero-body {
  display: block;
  height: 100vh;
  align-self: center;
  background-repeat: no-repeat;
  background-size: cover;
  background-attachment: fixed;
  align-items: center;
  justify-content: center;
}
</style>
