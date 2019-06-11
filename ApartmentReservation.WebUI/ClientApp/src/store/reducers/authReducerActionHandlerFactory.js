import * as constants from "../actions/constants";
import ActionHandlerFactory from "./actionHandlerFactory";

const successHandler = (state, action) => {
  return {
    ...state,
    user: action.user,
    authError: null
  };
};

const errorHandler = (state, action) => {
  return {
    ...state,
    user: null,
    authError: action.err.message
  };
};

const factory = new ActionHandlerFactory();
factory.register(constants.LOGIN_SUCCESS, successHandler);
factory.register(constants.LOGOUT_SUCCESS, successHandler);
factory.register(constants.REGISTER_SUCCESS, successHandler);
factory.register(constants.LOGIN_ERROR, errorHandler);
factory.register(constants.LOGOUT_ERROR, errorHandler);
factory.register(constants.REGISTER_ERROR, errorHandler);

export default factory;
