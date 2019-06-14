import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const apartmentService = {
  getAll
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Apartments${query}`, requestOptions).then(handleResponse);
}
