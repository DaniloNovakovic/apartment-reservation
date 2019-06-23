import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const commentService = {
  getAll,
  canPostComment
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Comments${query}`, requestOptions).then(handleResponse);
}

function canPostComment(apartmentId, guestId) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify({ apartmentId, guestId });

  return fetch(`api/Comments/CanPostComment${query}`, requestOptions).then(
    handleResponse
  );
}
