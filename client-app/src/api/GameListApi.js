import axios from "axios";

export default {
  fetchGameListDetails: (gameListId) => {
    return axios.get('http://localhost:5000/api/GameLists/' + gameListId)
      .then(response => response)
      .catch(error => error)
  },

  createGameList: (gameList) => {
    return axios.post('http://localhost:5000/api/GameLists', gameList)
      .then(response => response)
      .catch(error => error)
  },

  addGameToGameList(gameListId, game) {
    return axios.post('http://localhost:5000/api/GameLists/' + gameListId + '/add', game)
      .then(response => response)
      .catch(error => error)
  }
}

