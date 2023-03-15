// import { useEffect, useState } from "react";
// import "./App.css";

// import AddForm from "./AddForm";
// import OrdersService from "./api/OrdersService";
// import OrdersList from "./OrdersList";

// function App() {
//   const [orders, setOrders] = useState([]);
//   const [showForm, setShowForm] = useState(false);
//   const [edited, setEdited] = useState({});

//   function addOrder(newOrder) {
//     const id = OrdersService.createOrder(newOrder);
//     newOrder.id = id;
//     setOrders([...orders, newOrder]);
//   }
//   function removeOrder(removedOrder) {
//     OrdersService.deleteOrder(removedOrder.id);
//     setOrders(orders.filter((p) => p.id !== removedOrder.id));
//   }
//   function addnewOrder() {
//     setShowForm(!showForm);
//   }
//   function editOrder(editedOrder) {
//     OrdersService.updateOrder(editedOrder);
//     let newArr = [];
//     orders.map((order) => {
//       if (order.id === editedOrder.id) {
//         order.userId = editedOrder.userId;
//         order.shipAddress = editedOrder.shipAddress;
//         order.shipDate = editedOrder.shipDate;
//       }
//       newArr.push(order);
//     });

//     setOrders([...newArr]);
//   }

//   useEffect(() => {
//     getApi();
//   }, []);
//   async function getApi() {
//     const response = await OrdersService.getAll();
//     setOrders(response);
//   }

//   return (
//     <div className="App">
//       <div className="App__inner">
//         <h1 className="list-title">Orders available</h1>
//         <OrdersList orders={orders} remove={removeOrder} edit={editOrder} />
//         <button className="hotel-btn addHotel-btn" onClick={addnewOrder}>
//           Add new
//         </button>

//         {showForm && (
//           <AddForm
//             add={addOrder}
//             edited={edited}
//             setShowForm={setShowForm}
//             showForm={showForm}
//           />
//         )}
//       </div>
//     </div>
//   );
// }

// export default App;


import React from 'react';
import Orders from './Orders';

function App() {
  return (
    <div className="App">
      <Orders />
    </div>
  );
}

export default App;
