import { makeCancelable } from "../helpers";

class CountryService {
  static countries = [];
}

export const countriesService = {
  getCountries: () => makeCancelable(getCountries()),
  mapCountryCodeToCountryName: (...params) =>
    makeCancelable(mapCountryCodeToCountryName(...params))
};

function mapCountryCodeToCountryName(countryCode = "") {
  return getCountries(countryCode).then(countries => {
    const countryCodeUpper = countryCode.toUpperCase();
    const index = countries
      .map(item => item.value.toUpperCase())
      .lastIndexOf(countryCodeUpper);
    if (index < 0) {
      return "";
    }
    return countries[index].text;
  });
}

function getCountries() {
  if (CountryService.countries.length > 0) {
    return new Promise(resolve => {
      resolve(CountryService.countries);
    });
  }

  return fetch("./countries.json")
    .then(req => req.json())
    .then(json => {
      CountryService.countries = json;
      return [...CountryService.countries];
    })
    .catch(err => console.error(err));
}
