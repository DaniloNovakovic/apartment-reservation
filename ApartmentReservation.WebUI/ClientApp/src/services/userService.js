import { queryStringify } from "../helpers/queryHelpers";
import { createResponseHandler } from "../helpers/responseHandlerFactory";

export const handleResponse = createResponseHandler(logout);

export const userService = {
  login,
  logout,
  register,
  getAll,
  getById,
  update,
  updateCurrentUser,
  ban,
  unban,
  delete: _delete
};

export function login(username, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password })
  };

  return fetch(`api/Account/Login`, requestOptions)
    .then(handleResponse)
    .then(user => {
      // store user details in local storage to keep user logged in between page refreshes
      localStorage.setItem("user", JSON.stringify(user));

      return user;
    });
}

export function logout() {
    return fetch("api/Account/Logout").then(_ => localStorage.clear());
}

function getAll(filters = {}) {
  const requestOptions = {
    method: "GET"
  };
  let query = queryStringify(filters);

  return fetch(`api/Users${query}`, requestOptions).then(handleResponse);
}

function getById(id) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`api/Users/${id}`, requestOptions).then(handleResponse);
}

function register(user) {
  const requestOptions = {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(user)
  };

  return fetch(`api/Account/Register`, requestOptions).then(handleResponse);
}

function updateCurrentUser(newUser) {
  localStorage.setItem("user", JSON.stringify(newUser));
  return update(newUser);
}

function update(user) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user)
  };

  return fetch(`api/Users/${user.id}`, requestOptions).then(handleResponse);
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
  const requestOptions = {
    method: "DELETE"
  };

  return fetch(`api/Users/${id}`, requestOptions).then(handleResponse);
}

function ban(id) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`api/Users/${id}/Ban`, requestOptions).then(handleResponse);
}

function unban(id) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`api/Users/${id}/Unban`, requestOptions).then(handleResponse);
}
