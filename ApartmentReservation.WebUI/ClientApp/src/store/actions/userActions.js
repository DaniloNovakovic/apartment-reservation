import { userConstants } from "../../constants";
import { userService } from "../../services";
import { alertActions } from "./";
import { history } from "../../helpers";

export const logout = () => {
  return dispatch => {
    return userService.logout().then(_ => {
      dispatch({ type: userConstants.LOGOUT_SUCCESS });
      history.push("/");
    });
  };
};

export const login = (username, password) => {
  return dispatch => {
    return userService.login(username, password).then(
      user => {
        dispatch({ type: userConstants.LOGIN_SUCCESS, user });
        history.push("/");
      },
      error => {
        dispatch({ type: userConstants.LOGIN_ERROR, error });
        dispatch(alertActions.error(error.toString()));
      }
    );
  };
};

export const signup = user => {
  return dispatch => {
    return userService.register(user).then(
      user => {
        dispatch({ type: userConstants.REGISTER_SUCCESS, user });
        dispatch(alertActions.success("Registration successful"));
        dispatch(login(user.username, user.password));
      },
      error => {
        dispatch({ type: userConstants.REGISTER_ERROR, error });
        dispatch(alertActions.error(error.toString()));
      }
    );
  };
};

export const updateCurrentUser = user => {
  return dispatch => {
    return userService.updateCurrentUser(user).then(
      _ => {
        dispatch({ type: userConstants.UPDATE_CURRENT_USER, user });
        dispatch(alertActions.success("Successfully updated user info"));
      },
      error => {
        dispatch(alertActions.error(error.toString()));
      }
    );
  };
};
