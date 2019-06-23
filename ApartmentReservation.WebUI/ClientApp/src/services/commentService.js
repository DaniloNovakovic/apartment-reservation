import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const commentService = {
  getAll
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Comments${query}`, requestOptions).then(handleResponse);
}
