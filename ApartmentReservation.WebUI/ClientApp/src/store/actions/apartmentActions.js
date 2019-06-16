import { apartmentConstants } from "../../constants";
import { apartmentService } from "../../services";
import { alertActions } from "./";
import { history } from "../../helpers";

export const createApartment = (hostId, apartment) => {
  return dispatch => {
    return apartmentService.post({ hostId, ...apartment }).then(
      response => {
        dispatch({
          type: apartmentConstants.CREATE_APARTMENT_SUCCESS,
          apartment
        });
        history.push(`/view-apartment/${response.id}`);
      },
      error => {
        dispatch(alertActions.error(JSON.stringify(error)));
      }
    );
  };
};

export const updateCurrentApartment = apartment => {
  return {
    type: apartmentConstants.UPDATE_CURRENT_APARTMENT,
    currentApartment: apartment
  };
};

export const setCurrentApartment = newApartment => {
  return {
    type: apartmentConstants.SET_CURRENT_APARTMENT,
    currentApartment: newApartment
  };
};
