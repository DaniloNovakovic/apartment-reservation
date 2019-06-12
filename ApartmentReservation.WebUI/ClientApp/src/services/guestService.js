import { handleResponse } from "./userService";

export const guestService = {
  create
};

function create(guest) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(guest)
  };

  return fetch(`api/Guests`, requestOptions).then(handleResponse);
}
