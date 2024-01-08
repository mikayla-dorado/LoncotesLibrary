const _apiUrl = "/api/checkouts";

export const getCheckouts = () => {
    return fetch(`${_apiUrl}`).then(res => res.json())
}

export const returnCheckout = (id) => {
    return fetch(`${_apiUrl}/${id}/return`, {
        method: "POST",
        headers: {
            "Content-Type" : "application/json"
        }
    })
}