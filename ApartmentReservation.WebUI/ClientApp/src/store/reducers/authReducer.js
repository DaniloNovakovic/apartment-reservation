import handlerFactory from "./authReducerActionHandlerFactory";

const initState = {};

const authReducer = (state = initState, action) => {
  const handle = handlerFactory.getHandler(action.type);
  const newState = handle(state, action);
  console.log("authReducer ", newState);
  return newState;
};

export default authReducer;
