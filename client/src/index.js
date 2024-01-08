import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import MaterialList from "./components/tickets/MaterialList";
import MaterialDetails from "./components/tickets/MaterialDetails";
import CreateMaterial from "./components/tickets/CreateMaterial";
import { PatronDetails } from "./components/tickets/PatronDetails";
import { PatronList } from "./components/tickets/PatronList";
import { EditPatron } from "./components/tickets/EditPatron";
import { CheckoutList } from "./components/tickets/CheckoutList";
import { AvailableCheckouts } from "./components/tickets/AvailableCheckouts";
import { CheckoutForm } from "./components/CheckoutForm";
import { OverdueCheckouts } from "./components/OverdueCheckouts";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />}>
        <Route path="materials">
          <Route index element={<MaterialList />} />
          <Route path=":id" element={<MaterialDetails />} />
          <Route path="create" element={<CreateMaterial />} />
          <Route path="browse" element={<AvailableCheckouts />} />
        </Route>
        <Route path="patrons">
          <Route index element={<PatronList />} />
          <Route path=":id" element={<PatronDetails />} />
          <Route path=":id/edit" element={<EditPatron />} />
        </Route>
        <Route path="checkouts">
          <Route index element={<CheckoutList />} /> 
          <Route path=":id/form" element={<CheckoutForm />} />
          <Route path="overdue" element={<OverdueCheckouts />} />
        </Route>
      </Route>
    </Routes>
  </BrowserRouter>,
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
