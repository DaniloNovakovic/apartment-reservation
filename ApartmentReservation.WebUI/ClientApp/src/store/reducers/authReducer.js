const initState = {};

const authReducer = (state = initState, action) => {
  let newState = {
    ...state,
    user: action.user
  };
  console.log("authReducer ", newState);
  return newState;
};

export default authReducer;
