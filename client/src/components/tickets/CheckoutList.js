import { useEffect, useState } from "react"
import { getCheckouts, returnCheckout } from "../../data/checkoutData"
import { Table } from "reactstrap"

export const CheckoutList = () => {
    const [checkouts, setCheckouts] = useState([])

    const getAndSetCheckouts = () => {
        getCheckouts().then(array => setCheckouts(array))
    }

    useEffect(() => {
        getAndSetCheckouts()
    }, [])

    const handleReturnBtn = (event, id) => {
        event.preventDefault()
        returnCheckout(id).then(() => getAndSetCheckouts())
    }

    return (
        <div className="checkout-container">
            <h4>Checkouts</h4>
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Material Name</th>
                        <th>Patron Name</th>
                        <th>Date Checked Out</th>
                    </tr>
                </thead>
                <tbody>
                    {checkouts.map(c => (
                        <tr key={`checkouts-${c.id}`}>
                            <th scope="row">{c.id}</th>
                            <th>{c?.material?.materialName}</th>
                            <th>{c?.patron?.firstName}</th>
                            {/* need to use .slice so we dont get the seconds, only the y/m/d date */}
                            <th>{c?.checkoutDate.slice(0, 10)}</th>
                            <th>{c.returnDate == null ? (
                                // Display a return button only if ReturnDate is null
                                <button onClick={event => handleReturnBtn(event, c.id)}>Return Checkout</button>
                            ) : (
                                // Display a message or empty space if not checked out
                                null
                            )}</th>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    )
}