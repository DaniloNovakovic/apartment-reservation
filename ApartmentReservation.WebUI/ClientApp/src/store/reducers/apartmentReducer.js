import handlerFactory from "./apartmentReducerActionHandlerFactory";

const apartmentReducer = (state = {}, action) => {
  const handle = handlerFactory.getHandler(action.type);
  const newState = handle(state, action);
  return newState;
};

export default apartmentReducer;
