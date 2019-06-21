import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const reservationService = {
  getAll,
  post
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Reservations${query}`, requestOptions).then(handleResponse);
}

function post(data) {
  const requestOptions = {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  };

  return fetch(`api/Reservations`, requestOptions).then(handleResponse);
}
