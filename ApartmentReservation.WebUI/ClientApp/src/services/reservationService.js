import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const reservationService = {
  getAll,
  post,
  deny,
  accept,
  complete,
  withdraw
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

function deny(reservationId) {
  const requestOptions = {
    method: "GET"
  };
  return fetch(`api/Reservations/${reservationId}/Deny`, requestOptions).then(
    handleResponse
  );
}

function accept(reservationId) {
  const requestOptions = {
    method: "GET"
  };
  return fetch(`api/Reservations/${reservationId}/Accept`, requestOptions).then(
    handleResponse
  );
}

function complete(reservationId) {
  const requestOptions = {
    method: "GET"
  };
  return fetch(
    `api/Reservations/${reservationId}/Complete`,
    requestOptions
  ).then(handleResponse);
}

function withdraw(reservationId) {
  const requestOptions = {
    method: "GET"
  };
  return fetch(
    `api/Reservations/${reservationId}/Withdraw`,
    requestOptions
  ).then(handleResponse);
}
