import axios from "axios";

export default {
  registerUser: (registerData) => {
    return axios.post("http://localhost:5000/api/account/register", registerData)
      .then(response => response.data)
      .catch(error => error)
  },

  loginUser: (loginData) => {
    return axios.post("http://localhost:5000/api/account/login", loginData)
      .then(response => response.data)
      .catch(error => error)
  }
};
