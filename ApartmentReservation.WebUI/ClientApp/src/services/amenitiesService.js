import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const amenitiesService = {
  getAll,
  getById,
  create,
  update,
  delete: _delete
};

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Amenities${query}`, requestOptions).then(handleResponse);
}

function getById(id) {
  const requestOptions = {
    method: "GET"
  };
  return fetch(`api/Amenities/${id}`, requestOptions).then(handleResponse);
}

function create(amenity) {
  const requestOptions = {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(amenity)
  };

  return fetch(`api/Amenities`, requestOptions).then(handleResponse);
}

function update(amenity) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(amenity)
  };

  return fetch(`api/Amenities/${amenity.id}`, requestOptions).then(
    handleResponse
  );
}

function _delete(id) {
  const requestOptions = {
    method: "DELETE"
  };

  return fetch(`api/Amenities/${id}`, requestOptions).then(handleResponse);
}
