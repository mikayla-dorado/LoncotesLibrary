import { useState, useEffect } from "react"
import { Link, useNavigate } from "react-router-dom"
import { getAvailableMaterial } from "../../data/materialsData"
import { Table } from "reactstrap"

export const AvailableCheckouts = () => {
    const [available, setAvailable] = useState([])
    const navigate = useNavigate()

    const getAndSetAvailable = () => {
        getAvailableMaterial().then(array => setAvailable(array))
    }

    useEffect(() => {
        getAndSetAvailable()
    }, [])

    const handleCheckoutBtn = (event, id) => {
        event.preventDefault()
        navigate(`/checkouts/${id}/form`)
    }

    return (
        <div className="available-container">
            <h4>Available Materials</h4>
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Genre</th>
                        <th>Type</th>
                    </tr>
                </thead>
                <tbody>
                    {available.map(m => (
                        <tr key={`materials-${m.id}`}>
                            <th scope="row">{m.id}</th>
                            <td>{m.materialName}</td>
                            <td>{m.genre.name}</td>
                            <td>{m.materialType.name}</td>
                            <td>
                                <Link to={`${m.id}`}>Details</Link>
                            </td>
                            <td><button onClick={event => handleCheckoutBtn(event, m.id)}>Checkout Material</button></td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    )
}