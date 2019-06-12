import { userConstants } from "../../constants";
import { userService } from "../../services";
import { alertActions } from "./";
import { history } from "../../helpers";

export const logout = () => {
  return dispatch => {
    userService.logout();
    dispatch({ type: userConstants.LOGOUT_SUCCESS });
  };
};

export const login = (username, password) => {
  return dispatch => {
    userService.login(username, password).then(
      user => {
        dispatch({ type: userConstants.LOGIN_SUCCESS, user });
        history.push("/");
      },
      error => {
        dispatch({ type: userConstants.LOGIN_ERROR, err: error });
        dispatch(alertActions.error(error.toString()));
      }
    );
  };
};

export const signup = user => {
  return dispatch => {
    userService.register(user).then(
      user => {
        dispatch({ type: userConstants.REGISTER_SUCCESS, user });
        dispatch(alertActions.success("Registration successful"));
      },
      error => {
        dispatch({ type: userConstants.REGISTER_ERROR, err: error });
        dispatch(alertActions.error(error.toString()));
      }
    );
  };
};
