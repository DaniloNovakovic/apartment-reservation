import handlerFactory from "./authReducerActionHandlerFactory";

let user = JSON.parse(localStorage.getItem("user"));
const initState = user ? { loggedIn: true, user } : {};

const authReducer = (state = initState, action) => {
  const handle = handlerFactory.getHandler(action.type);
  const newState = handle(state, action);
  return newState;
};

export default authReducer;
