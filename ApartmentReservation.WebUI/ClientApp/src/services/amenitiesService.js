import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const amenitiesService = {
  getAll
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Amenities${query}`, requestOptions).then(handleResponse);
}
