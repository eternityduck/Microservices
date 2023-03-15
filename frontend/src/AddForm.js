import React, { useState } from "react";

export default function AddForm({ add, setShowForm, showForm }) {
  const [orderUserId, setOrderUserId] = useState("");
  const [orderShipAddress, setOrderShipAddress] = useState("");
  const [orderShipDate, setOrderShipDate] = useState("");

  function handleSumbit(e) {
    e.preventDefault();

    const newOrder = {
      userId: orderUserId,
      shipAddress: orderShipAddress,
      shipDate: orderShipDate,
    };

    add(newOrder);

    setOrderUserId("");
    setOrderShipAddress("");
    setOrderShipDate("");
    setShowForm(!showForm);
  }
  return (
    <div>
      <form className="hotels-form" onSubmit={handleSumbit}>
        <div className="hotels-form__inner">
          <input
            className="hotels-form__inner__input"
            type="text"
            value={orderUserId}
            placeholder="User Id"
            onChange={(e) => setOrderUserId(e.target.value)}
            required
          />
          <input
            className="hotels-form__inner__input"
            type="number"
            value={orderShipAddress}
            placeholder="Ship Address"
            onChange={(e) => setOrderShipAddress(e.target.value)}
            required
          />
          <input
            className="hotels-form__inner__input"
            type="text"
            value={orderShipDate}
            placeholder="Ship Date"
            onChange={(e) => setOrderShipDate(e.target.value)}
            required
          />
          <button className="hotel-btn hotel-form-btn">Edit</button>
        </div>
      </form>
    </div>
  );
}
