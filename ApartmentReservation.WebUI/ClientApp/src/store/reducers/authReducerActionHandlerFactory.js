import { userConstants } from "../../constants";
import ActionHandlerFactory from "./actionHandlerFactory";

const loginSuccessHandler = (state, action) => {
  return {
    ...state,
    user: action.user,
    loggedIn: true
  };
};
const logoutSuccessHandler = (state, action) => {
    return {
        ...state,
        user: null,
        loggedIn: false
    };
}

const errorHandler = (state, action) => {
  return {
    ...state,
    user: null,
    loggedIn: false
  };
};

const updateCurrentUserHandler = (state, action) => {
  return {
    ...state,
    user: action.user
  };
};

const factory = new ActionHandlerFactory();
factory.register(userConstants.LOGIN_SUCCESS, loginSuccessHandler);
factory.register(userConstants.LOGOUT_SUCCESS, logoutSuccessHandler);
factory.register(userConstants.LOGIN_ERROR, errorHandler);
factory.register(userConstants.LOGOUT_ERROR, errorHandler);
factory.register(userConstants.REGISTER_ERROR, errorHandler);
factory.register(userConstants.UPDATE_CURRENT_USER, updateCurrentUserHandler);

export default factory;
