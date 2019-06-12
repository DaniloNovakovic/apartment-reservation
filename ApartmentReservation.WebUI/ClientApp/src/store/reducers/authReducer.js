import handlerFactory from "./authReducerActionHandlerFactory";

// for testing only (set to empty later)...
const initState = {
  user: {
    firstName: null,
    gender: null,
    id: 1,
    lastName: null,
    password: "admin",
    roleName: "Administrator",
    username: "admin"
  }
};

const authReducer = (state = initState, action) => {
  const handle = handlerFactory.getHandler(action.type);
  const newState = handle(state, action);
  console.log("authReducer ", newState);
  return newState;
};

export default authReducer;
