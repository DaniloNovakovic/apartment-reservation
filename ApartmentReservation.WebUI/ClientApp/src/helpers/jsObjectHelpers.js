export function filterObjByUnallowedPropNames(
  objToFilter,
  ...unallowedPropNames
) {
  const filtered = Object.keys(objToFilter)
    .filter(key => !unallowedPropNames.includes(key))
    .reduce((obj, key) => {
      return {
        ...obj,
        [key]: obj[key]
      };
    }, {});

  return filtered;
}

export function filterObjByAllowedPropNames(objToFilter, ...allowedPropNames) {
  const filtered = Object.keys(objToFilter)
    .filter(key => allowedPropNames.includes(key))
    .reduce((obj, key) => {
      return {
        ...obj,
        [key]: objToFilter[key]
      };
    }, {});

  return filtered;
}
