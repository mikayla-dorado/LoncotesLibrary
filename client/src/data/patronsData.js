const _apiUrl = "/api/patrons";

export const getPatrons = () => {
    return fetch(_apiUrl).then((r) => r.json());
};

export const getPatronsById = (id) => {
    return fetch(`${_apiUrl}/${id}`).then((r) => r.json());
};