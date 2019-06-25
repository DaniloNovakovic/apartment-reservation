import { queryStringify } from "../helpers/queryHelpers";
import { handleResponse } from "./userService";

export const apartmentService = {
  getAll,
  getById,
  post,
  put,
  updateAmenities,
  updateForRentalDates,
  addImages,
  deleteImages,
  delete: _delete
};

function addImages(apartmentId, formData) {
  const requestOptions = {
    method: "POST",
    body: formData
  };
  return fetch(`api/Apartments/${apartmentId}/Images`, requestOptions).then(
    handleResponse
  );
}

function deleteImages(apartmentId, images) {
  const requestOptions = {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ images })
  };
  return fetch(
    `api/Apartments/${apartmentId}/delete-images`,
    requestOptions
  ).then(handleResponse);
}

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

function put(data) {
  const requestOptions = {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  };

  return fetch(`api/Apartments/${data.id}`, requestOptions).then(
    handleResponse
  );
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

  return fetch(`api/Apartments`, requestOptions).then(handleResponse);
}

function _delete(id) {
  const requestOptions = {
    method: "DELETE"
  };

  return fetch(`api/Apartments/${id}`, requestOptions).then(handleResponse);
}

function updateAmenities(data) {
  const requestOptions = {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  };

  return fetch(`api/Apartments/${data.id}/Amenities`, requestOptions).then(
    handleResponse
  );
}

function updateForRentalDates(data) {
  const requestOptions = {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  };

  return fetch(`api/Apartments/${data.id}/ForRentalDates`, requestOptions).then(
    handleResponse
  );
}
