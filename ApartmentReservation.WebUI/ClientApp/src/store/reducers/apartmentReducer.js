import handlerFactory from "./apartmentReducerActionHandlerFactory";

const initState = {
  currentApartment: {}
};

const apartmentReducer = (state = initState, action) => {
  const handle = handlerFactory.getHandler(action.type);
  const newState = handle(state, action);
  return newState;
};

export default apartmentReducer;
