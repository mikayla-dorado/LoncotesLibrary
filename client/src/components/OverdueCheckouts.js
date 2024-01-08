import { useEffect, useState } from "react"
import { getOverdueCheckouts } from "../data/checkoutData"
import { Table } from "reactstrap"

export const OverdueCheckouts = () => {
    const [overdueCheckouts, setOverdueCheckouts] = useState([])

    const getAndSetOverdueCheckouts = () => {
        getOverdueCheckouts().then(array => setOverdueCheckouts(array))
    }

    useEffect(() => {
        getAndSetOverdueCheckouts()
    }, [])

    return(
        <div className="overdue-container">
            <h4>Overdue Checkouts</h4>
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Material Name</th>
                        <th>Patron Name</th>
                        <th>Checkout Date</th>
                    </tr>
                </thead>
                <tbody>
                    {overdueCheckouts.map(c => (
                        <tr key={`checkouts-${c.id}`}>
                            <th scope="row">{c.id}</th>
                            <th>{c?.material?.materialName}</th>
                            <th>{c?.patron?.firstName}</th>
                            <th>{c?.checkoutDate?.slice(0,10)}</th>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    )
}