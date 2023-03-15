import React, { useState } from "react";
import EditForm from "./EditForm";

export default function ListItem(props) {
  console.log(props);
  const [showForm, setShowForm] = useState(false);
  const [edited, setEdited] = useState(null);
  function editOrder() {
    setShowForm(!showForm);
    setEdited(props.order);
  }
  function deleteOrder() {
    props.remove(props.order);
  }

  return (
    <div className="ListItem">
      <div className="ListItem__inner">
        <div className="hotel-name">
          {props.number}
          {"."} {props.order.id}
        </div>
        <div className="hotel-btns">
          <button className="hotel-btn" onClick={editOrder}>
            edit
          </button>
          <button className="hotel-btn" onClick={deleteOrder}>
            del
          </button>
        </div>
      </div>
      {edited && showForm && (
        <EditForm
          edited={edited}
          edit={props.edit}
          showForm={showForm}
          setShowForm={setShowForm}
        />
      )}
    </div>
  );
}
