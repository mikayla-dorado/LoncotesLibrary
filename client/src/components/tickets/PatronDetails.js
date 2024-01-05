import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { getPatronsById } from "../../data/patronsData"
import { Table } from "reactstrap";


export const PatronDetails = () => {
    const { id } = useParams();
    const [patron, setPatron] = useState(null)

    useEffect(() => {
        getPatronsById(id).then(obj => setPatron(obj))
    }, [id])

    return (
        <div className="patron-details-container">
            <h2>{patron.firstName} {patron.lastName}</h2>
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Address</th>
                        <th>Email</th>
                        <th>Active</th>
                        <th>Balance</th>
                    </tr>
                </thead>
                <tbody>
                    <th scope="row">{patron.id}</th>
                    <th>{patron.firstName}</th>
                    <th>{patron.lastName}</th>
                    <th>{patron.address}</th>
                    <th>{patron.email}</th>
                    <th>{patron.isActive ? 'true' : 'false '}</th>
                    <th>${patron.balance}</th>
                </tbody>
            </Table>

        </div>
    )
}