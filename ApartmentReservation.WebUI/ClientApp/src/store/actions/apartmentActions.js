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

export const deleteApartment = apartmentId => {
  return dispatch => {
    return apartmentService.delete(apartmentId).then(
      _ => {
        history.push("/");
      },
      error => {
        dispatch(alertActions.error(JSON.stringify(error)));
      }
    );
  };
};

export const updateCurrentApartmentAmenities = apartmentAmenityData => {
  return dispatch => {
    return apartmentService.updateAmenities(apartmentAmenityData).then(
      _ => {
        dispatch({
          type: apartmentConstants.UPDATE_CURRENT_APARTMENT,
          currentApartment: apartmentAmenityData
        });
        dispatch(
          alertActions.success(
            `Successfully updated apartment '${apartmentAmenityData.id}'`
          )
        );
      },
      error => {
        dispatch(alertActions.error(JSON.stringify(error)));
      }
    );
  };
};

export const updateCurrentApartmentRentalDates = rentalDatesData => {
  return dispatch => {
    return apartmentService.updateForRentalDates(rentalDatesData).then(
      _ => {
        dispatch({
          type: apartmentConstants.UPDATE_CURRENT_APARTMENT,
          currentApartment: rentalDatesData
        });
        dispatch(
          alertActions.success(
            `Successfully updated apartment '${rentalDatesData.id}'`
          )
        );
      },
      error => {
        dispatch(alertActions.error(JSON.stringify(error)));
      }
    );
  };
};

export const updateCurrentApartment = apartmentData => {
  return dispatch => {
    return apartmentService.put(apartmentData).then(
      _ => {
        dispatch({
          type: apartmentConstants.UPDATE_CURRENT_APARTMENT,
          currentApartment: apartmentData
        });
        dispatch(
          alertActions.success(
            `Successfully updated apartment '${apartmentData.id}'`
          )
        );
      },
      error => {
        dispatch(alertActions.error(JSON.stringify(error)));
      }
    );
  };
};

export const setCurrentApartment = newApartment => {
  return {
    type: apartmentConstants.SET_CURRENT_APARTMENT,
    currentApartment: newApartment
  };
};
