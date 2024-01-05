import { useEffect, useState } from "react"
import { getPatrons } from "../../data/patronsData"
import { Table } from "reactstrap";
import { Link } from "react-router-dom";


export const PatronList = () => {
    const [patrons, setPatrons] = useState([]);

    const getAndSetPatrons = () => {
        getPatrons().then(array => setPatrons(array))
    }

    useEffect(() => {
        getAndSetPatrons()
    }, [])

    return (
        <div className="patron-list-container">
            <h4>Patrons</h4>
            <Table>
                <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Address</th>
                    <th>Email</th>
                    <th>Active</th>
                </tr>
                </thead>
                <tbody>
                    {patrons.map((p) => (
                        <tr key={`materials-${p.id}`}>
                            <th scope="row">{p.id}</th>
                            <td>{p.firstName}</td>
                            <td>{p.lastName}</td>
                            <td>{p.address}</td>
                            <td>{p.email}</td>
                            <td>{p.isActive ? 'true' : 'false'}</td>
                            <td>
                                <Link to={`${p.id}`}>Details</Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    )
}