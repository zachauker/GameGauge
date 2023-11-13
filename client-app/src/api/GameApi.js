import axios from "axios";

export default {
  getGameDetails: (gameId) => {
    return axios.get("http://localhost:5000/api/games/" + gameId)
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