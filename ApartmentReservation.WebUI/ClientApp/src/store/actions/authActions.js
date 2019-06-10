import * as constants from "./constants";

export const logout = () => {
  return {
    type: constants.LOGOUT_SUCCESS
  };
};

export const login = user => {
  return {
    type: constants.LOGIN_SUCCESS,
    user: { ...user, id: 1 }
  };
};

export const signup = user => {
  return {
    type: constants.REGISTER_SUCCESS,
    user: { ...user, id: 1 }
  };
};
