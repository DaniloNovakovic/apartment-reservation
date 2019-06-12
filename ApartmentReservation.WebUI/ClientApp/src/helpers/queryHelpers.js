export function queryStringify(filters = {}) {
  let query = "?";
  for (let filterName in filters) {
    let keyValPair = `${filterName}=${filters[filterName]}`;
    if (query != "?") {
      query += "&";
    }
    query += keyValPair;
  }
  return query;
}
