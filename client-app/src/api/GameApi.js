import axios from "axios";

export default {
  getGameDetails: (gameId) => {
    return axios.get("http://localhost:5000/api/games/" + gameId)
      .then(response => response)
      .catch(error => error)
  },

  getGameArtworks: (gameId) => {
    return axios.get("http://localhost:5000/api/media/artwork/" + gameId)
      .then(response => response)
      .catch(error => error)
  },

  getGameScreenshots: (gameId) => {
    return axios.get("http://localhost:5000/api/media/screenshots/" + gameId)
      .then(response => response)
      .catch(error => error)
  },

  getGameVideos: (gameId) => {
    return axios.get("http://localhost:5000/api/media/videos/" + gameId)
      .then(response => response)
      .catch(error => error)
  },

  searchGames: (query) => {
    return axios.get("http://localhost:5000/api/games/search", {params: {
        SearchTerm: query.SearchTerm,
        PageSize: query.PageSize,
        PageNumber: query.PageNumber
      }})
      .then(response => response.data)
      .catch(error => error)
  }
}
