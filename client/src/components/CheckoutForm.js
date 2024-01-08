import { useState } from "react"
import { useNavigate, useParams } from "react-router-dom"

export const CheckoutForm = () => {
    const [checkout, setCheckout] = useState([])
    const [material, setMaterial] = useState({})


    const handleCheckoutBtn  = (event) => {
        event.preventDefault()
    }
//need to add in the functionality for the form

    return (
        <div className="checkoutform-container">
            <h4>Checkout Form</h4>
            <p>Enter your Patron Id to checkout a material</p>
            <input
            type ="text"
            onChange={event => {
                const checkoutCopy = {...checkout}
                checkoutCopy.patronId = parseInt(event.target.value)
                setCheckout(checkoutCopy)
            }}
            />
            <button onClick={event => handleCheckoutBtn(event)}>Checkout</button>
        </div>
    )
}