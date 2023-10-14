import axios from "axios";

export function fetchGameListDetails(gameListId)
{
  return axios.get('http://localhost:5000/api/GameLists/' + gameListId)
    .then(response => response)
    .catch(error => error)
}

export function createGameList(gameList)
{
  return axios.post('http://localhost:5000/api/GameLists', gameList)
    .then(response => response)
    .catch(error => error)
}

