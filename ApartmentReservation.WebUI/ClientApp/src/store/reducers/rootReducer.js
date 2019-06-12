import authReducer from "./authReducer";
import projectReducer from "./projectReducer";
import alertReducer from "./alertReducer";
import { combineReducers } from "redux";

const rootReducer = combineReducers({
  auth: authReducer,
  project: projectReducer,
  alert: alertReducer
});

export default rootReducer;
