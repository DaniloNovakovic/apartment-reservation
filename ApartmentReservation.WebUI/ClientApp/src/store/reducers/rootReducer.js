import authReducer from "./authReducer";
import projectReducer from "./projectReducer";
import alertReducer from "./alertReducer";
import apartmentReducer from "./apartmentReducer";
import { combineReducers } from "redux";

const rootReducer = combineReducers({
  auth: authReducer,
  project: projectReducer,
  alert: alertReducer,
  apartment: apartmentReducer
});

export default rootReducer;
