import { apartmentConstants } from "../../constants";
import ActionHandlerFactory from "./actionHandlerFactory";

const updateCurrentApartment = (state, action) => {
  return {
    ...state,
    currentApartment: {
      ...state.currentApartment,
      ...action.currentApartment
    }
  };
};

const setCurrentApartment = (state, action) => {
  return {
    ...state,
    currentApartment: action.currentApartment
  };
};

const factory = new ActionHandlerFactory();
factory.register(
  apartmentConstants.UPDATE_CURRENT_APARTMENT,
  updateCurrentApartment
);
factory.register(apartmentConstants.SET_CURRENT_APARTMENT, setCurrentApartment);

export default factory;
