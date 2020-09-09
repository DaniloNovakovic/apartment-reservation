import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const commentService = {
  canPostComment,
  post,
  approve,
};

function approve(commentId) {
  const requestOptions = {
    method: "GET",
  };
  return fetch(`api/Comments/${commentId}/Approve`, requestOptions).then(
    handleResponse
  );
}

function canPostComment(apartmentId, guestId) {
  const requestOptions = {
    method: "GET",
  };
  let query = queryStringify({ apartmentId, guestId });

  return fetch(`api/Comments/CanPostComment${query}`, requestOptions).then(
    handleResponse
  );
}

function post(data) {
  const requestOptions = {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };

  return fetch(`api/Comments`, requestOptions).then(handleResponse);
}
