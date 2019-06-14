import { userConstants } from "../../constants";
import ActionHandlerFactory from "./actionHandlerFactory";

const successHandler = (state, action) => {
  return {
    ...state,
    user: action.user,
    loggedIn: true
  };
};

const errorHandler = (state, action) => {
  return {
    ...state,
    user: null,
    loggedIn: false
  };
};

const factory = new ActionHandlerFactory();
factory.register(userConstants.LOGIN_SUCCESS, successHandler);
factory.register(userConstants.LOGOUT_SUCCESS, successHandler);
factory.register(userConstants.REGISTER_SUCCESS, successHandler);
factory.register(userConstants.LOGIN_ERROR, errorHandler);
factory.register(userConstants.LOGOUT_ERROR, errorHandler);
factory.register(userConstants.REGISTER_ERROR, errorHandler);

export default factory;
