import { queryStringify } from "../helpers/queryHelpers";

export const userService = {
  login,
  logout,
  register,
  getAll,
  getById,
  update,
  delete: _delete
};

function login(username, password) {
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

function logout() {
  fetch("api/Account/Logout");
  localStorage.removeItem("user");
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

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
