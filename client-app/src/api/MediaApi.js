import axios from 'axios'
import { getToken } from '../api/TokenStorage'

const token = getToken()

export default {
  getGameCoverImage: (coverId) => {
    let coverUrl = 'https://images.igdb.com/igdb/image/upload/cover_big/' + coverId + '.jpg'
    return axios.post(coverUrl, {
      headers: {
        'Authorization': 'Bearer ' + token
      },
      params: {

      }
    })
  }
}
