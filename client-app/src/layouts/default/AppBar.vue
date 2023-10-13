<template>
  <v-app-bar :absolute="true" :flat="true" color="primary">
    <v-row align="center" justify="center">
      <v-col cols="auto">
        <v-app-bar-title>
          <v-icon icon=""/>
          GameGauge
        </v-app-bar-title>
      </v-col>
      <v-col cols="auto" v-for="item in navigationItems" class="nav-item link-hover">
        <router-link :to="item.route">
          <span class="text-h6 nav-link">{{ item.text }}</span>
        </router-link>
      </v-col>
    </v-row>
  </v-app-bar>
</template>

<script>
export default {
  data() {
    return {
      navigationItems: [
        {"text": "Home", route: "/"},
        {"text": "About", route: "/about"},
        {"text": "Contact", route: "/contact"},
        {"text": "Trending", route: "/trending"}
      ]
    }
  }
}
</script>

<style scoped>
a {
  text-decoration: none;
}


a.nav-item:hover {
  background-color: #373f45;
}

.nav-item {
  color: white;
}

.link-hover {
  display: flex;
  text-decoration: none;
  position: relative;
  background-color: #864de8;
  cursor: pointer;
  color: inherit;
}

.link-hover:before {
  position: absolute;
  left: 0;
  bottom: 0;
  content: "";
  display: block;
  width: 100%;
  height: 100%;
  background-color: #373f45;
  transform-origin: 0 bottom 0;
  transform: scaleY(0);
  transition: 0.4s ease-out;
}

.link-hover:hover:before {
  transform: scaleY(1);
}

.nav-link {
  position: relative;
  transition: 0.4s ease-out;
  color: white;
}

.nav-link {
  position: relative;
  transition: 0.4s ease-out;
  color: white;
}
</style>
