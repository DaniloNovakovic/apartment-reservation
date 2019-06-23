export function queryStringify(filters = {}) {
  let query = "?";
  for (let filterName in filters) {
    if (!filters[filterName]) continue;
    let keyValPair = `${filterName}=${filters[filterName]}`;
    if (query !== "?") {
      query += "&";
    }
    query += keyValPair;
  }
  return query;
}
