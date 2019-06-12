import { handleResponse } from "./userService";

export const hostService = {
  create
};

function create(guest) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(guest)
  };

  return fetch(`api/Hosts`, requestOptions).then(handleResponse);
}
