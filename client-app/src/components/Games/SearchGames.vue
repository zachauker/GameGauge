<script setup>
import {ref} from "vue"
import {debounce} from "lodash"
import {useSearchStore} from "@/store/search"

const searchStore = useSearchStore()

const query = ref({
  "SearchTerm": '',
  "PageNumber": 1,
  "PageSize": 50
})
const error = ref('')
const isSearching = ref(false)

const search = async () => {
  try {
    error.value = ''
    isSearching.value = true
    searchStore.searchGames(query.value)
        .then(() => {
          isSearching.value = true
        })
  } catch (err) {
    console.error('There was an error:', err)
    error.value = 'Failed to retrieve results.'
  }
};

const debouncedSearch = debounce(search, 1000);

const onInput = (searchTerm) => {
  if (searchTerm) {
    debouncedSearch();
  } else {
    searchStore.results = [];
  }
};
</script>

<template>
  <v-text-field
      v-model="query.SearchTerm"
      @update:modelValue="onInput"
      placeholder="Search for a game..."
      variant="outlined">
  </v-text-field>
</template>

<style scoped>

</style>
