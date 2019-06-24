import { history } from "./history";

export function createResponseHandler(logoutCallback) {
  return function handleResponse(response) {
    return response.text().then(text => {
      let data;
      try {
        data = text && JSON.parse(text);
      } catch (e) {
        console.error(e);
        return Promise.reject(
          `failed to parse '${text}' for response: ${response}`
        );
      }

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
