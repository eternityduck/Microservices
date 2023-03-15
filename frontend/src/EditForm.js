import React, { useState } from "react";

export default function EditForm(props) {
  console.log(props);
  const [orderUserId, setOrderUserId] = useState("");
  const [orderShipAddress, setOrderShipAddress] = useState("");
  const [orderShipDate, setOrderShipDate] = useState("");

  function handleSumbit(e) {
    e.preventDefault();

    const editedOrder = {
      userId: orderUserId,
      shipAddress: orderShipAddress,
      shipDate: orderShipDate,
      id: props.edited.id,
    };
    props.edit(editedOrder);

    setOrderUserId("");
    setOrderShipAddress("");
    setOrderShipDate("");
    props.setShowForm(!props.showForm);
  }
  return (
    <div>
      <form className="hotels-form" onSubmit={handleSumbit}>
        <div className="hotels-form__inner">
          <input
            className="hotels-form__inner__input"
            type="text"
            value={orderUserId}
            placeholder={props.edited.userId}
            onChange={(e) => setOrderUserId(e.target.value)}
            required
          />
          <input
            className="hotels-form__inner__input"
            type="number"
            value={orderShipAddress}
            placeholder={props.edited.price}
            onChange={(e) => setOrderShipAddress(e.target.value)}
            required
          />
          <input
            className="hotels-form__inner__input"
            type="text"
            value={orderShipDate}
            placeholder={props.edited.brand}
            onChange={(e) => setOrderShipDate(e.target.value)}
            required
          />
          <button className="hotel-btn hotel-form-btn">Edit</button>
        </div>
      </form>
    </div>
  );
}
