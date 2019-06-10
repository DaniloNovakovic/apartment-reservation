const initState = {
  apartments: []
};

const projectReducer = (state = initState, action) => {
  console.log("projectReducer", state);
  return { ...state };
};

export default projectReducer;
