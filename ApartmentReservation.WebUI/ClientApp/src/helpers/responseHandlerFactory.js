import { history } from "./history";

export function createResponseHandler(logoutCallback) {
  return function handleResponse(response) {
    return response.text().then(text => {
      const data = text && JSON.parse(text);
      if (!response.ok) {
        if ([401, 403].indexOf(response.status) !== -1) {
          // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
          logoutCallback();
          history.push("/Account/Login");
        }

        const error = (data && data.error) || response.statusText || data;
        return Promise.reject(error);
      }

      return data;
    });
  };
}
