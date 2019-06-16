import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const apartmentService = {
  getAll,
  getById,
  post
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Apartments${query}`, requestOptions).then(handleResponse);
}

function getById(id) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`api/Apartments/${id}`, requestOptions).then(handleResponse);
}

function post(data) {
  console.log(data);
  const requestOptions = {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  };

  return fetch(`api/Apartments`, requestOptions).then(handleResponse);
}
