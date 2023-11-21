import axios from "axios";

export default {
  createReview: (review) => {
    return axios.post('http://localhost:5000/api/review', review)
      .then(response => response)
      .catch(error => error)
  }
}
