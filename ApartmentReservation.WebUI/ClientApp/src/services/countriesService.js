class CountryService {
  static countries = [];
}

export const countriesService = {
  getCountries
};

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
