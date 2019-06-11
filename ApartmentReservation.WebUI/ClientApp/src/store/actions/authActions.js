import * as constants from "./constants";

const parseResponse = async rawResponse => {
  console.log("response: " + rawResponse);
  let content = await rawResponse.json();
  if (!rawResponse.ok) {
    throw Error(content.error);
  }
  return content;
};

export const logout = () => {
  return async dispatch => {
    try {
      await fetch("api/Account/Logout");
      dispatch({ type: constants.LOGOUT_SUCCESS });
    } catch (err) {
      dispatch({ type: constants.LOGOUT_ERROR, err });
    }
  };
};

export const login = user => {
  return async dispatch => {
    try {
      const rawResponse = await fetch("api/Account/Login", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
      });
      const content = await parseResponse(rawResponse);
      console.log(content);
      return dispatch({ type: constants.LOGIN_SUCCESS, user: content });
    } catch (err) {
      return dispatch({ type: constants.LOGIN_ERROR, err });
    }
  };
};

export const signup = user => {
  return async dispatch => {
    try {
      const rawResponse = await fetch("api/Account/Register", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
      });
      const content = await parseResponse(rawResponse);
      return dispatch({ type: constants.REGISTER_SUCCESS, user: content });
    } catch (err) {
      return dispatch({ type: constants.REGISTER_ERROR, err });
    }
  };
};
