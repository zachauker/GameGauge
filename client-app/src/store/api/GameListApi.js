import axios from "axios";

export function fetchGameListDetails(gameListId)
{
  return axios.get('http://localhost:5000/api/GameLists/' + gameListId)
    .then(response => response)
    .catch(error => error)
}

