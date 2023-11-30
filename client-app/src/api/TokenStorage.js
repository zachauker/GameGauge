import apicalypse from 'apicalypse'
import axios from 'axios'
// tokenStorage.js
import SecureLS from 'secure-ls';

const client_id = import.meta.env.VITE_IGDB_CLIENT_ID
const client_secret = import.meta.env.VITE_IGDB_CLIENT_SECRET
const params = {
  'client_id': client_id,
  'client_secret': client_secret,
  'grant_type': 'client_credentials'
}
const ls = new SecureLS({isCompression: false});


export async function generateToken() {
  try {
    axios.post("https://id.twitch.tv/oauth2/token", params).then(response => {
      saveToken(response.data['access_token'])
    })
  } catch (err) {
    console.log(err)
  }
}

export function saveToken(token) {
  ls.set('accessToken', token);
}

export function getToken() {
  return ls.get('accessToken');
}

export function removeToken() {
  ls.remove('accessToken');
}
