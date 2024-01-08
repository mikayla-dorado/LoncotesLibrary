import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getMaterials } from "../data/materialsData";
import { checkoutMaterial } from "../data/checkoutData";

export const CheckoutForm = () => {
  const { id: materialId } = useParams();
  const navigate = useNavigate();
  const [material, setMaterial] = useState({});
  const [patronId, setPatronId] = useState("");

  useEffect(() => {
    getMaterials(materialId).then(obj => setMaterial(obj));
  }, [materialId]);

  const handleCheckoutBtn = async (event) => {
    event.preventDefault();

    if (!patronId) {
      window.alert("Please enter a valid Patron Id");
      return;
    }

    const checkoutData = { materialId, patronId: parseInt(patronId) };

    try {
      await checkoutMaterial(checkoutData);
      navigate("/materials/browse");
    } catch (error) {
      console.error("Checkout failed:", error);
      // Handle error if needed
    }
  };

  return (
    <div className="container">
      <div className="sub-menu bg-light">
        <h4>Checkouts Form</h4>
      </div>
      <div className="form-list">
        <h6>Enter Patron Id to Checkout a Material</h6>
        <input
          type="text"
          value={patronId}
          onChange={(event) => setPatronId(event.target.value)}
        />
        <button onClick={handleCheckoutBtn}>Checkout</button>
      </div>
    </div>
  );
};