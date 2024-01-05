import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { editPatron, getPatronsById } from "../../data/patronsData";
import { Button } from "reactstrap";

export const EditPatron = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [patron, setPatron] = useState(null)

    useEffect(() => {
        getPatronsById(id).then(obj => setPatron(obj))
    }, [id])

    const handleSubmit = (e) => {
        e.preventDefault();
        if (patron.email && patron.address) {
            editPatron(patron).then(navigate("/patrons"))
        } else {
            window.alert("Please continue filling out the form")
        }
    }

    return (
        <div className="edit-patron">
            <h2 className="edit">Edit {patron?.firstName}'s information?</h2>
            <form>
                <p>Email:</p>
                <input
                    type="text"
                    value={patron?.email}
                    onChange={e => {
                        const patronCopy = { ...patron }
                        patronCopy.email = e.target.value
                        setPatron(patronCopy)
                    }}
                />
                <p>Address:</p>
                <input
                    type="text"
                    value={patron?.address}
                    onChange={e => {
                        const patronCopy = { ...patron }
                        patronCopy.address = e.target.value
                        setPatron(patronCopy)
                    }}
                />
            </form>
            <Button onClick={handleSubmit}>Save Changes</Button>
        </div>
    )
}