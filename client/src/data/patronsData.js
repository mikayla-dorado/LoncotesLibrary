const _apiUrl = "/api/patrons";

export const getPatrons = () => {
    return fetch(_apiUrl).then((r) => r.json());
};

export const getPatronsById = (id) => {
    return fetch(`${_apiUrl}/${id}`).then((r) => r.json());
};

export const editPatron = (patron) => {
    return fetch(`${_apiUrl}/${patron.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(patron)
    })
}

export const deactivatePatron = (id) => {
    return fetch(`${_apiUrl}/${id}/deactivate`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }
    })
}

export const activatePatron = (id) => {
    return fetch(`${_apiUrl}/${id}/activate`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }
    })
}