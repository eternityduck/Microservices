import React from "react";
import ListItem from "./ListItem";

export default function OrdersList({ orders, remove, edit }) {
  console.log(orders);
  return (
    <div className="OrdersList">
      {orders.map(function (order, index) {
        return (
          <ListItem
            key={index}
            car={order}
            number={index + 1}
            remove={remove}
            edit={edit}
          />
        );
      })}
    </div>
  );
}
