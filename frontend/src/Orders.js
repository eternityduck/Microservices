import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Button, Modal, Form } from 'react-bootstrap';

const BASE_URL = 'http://localhost';

function Orders() {
  const [orders, setOrders] = useState([]);
  const [selectedOrder, setSelectedOrder] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [form, setForm] = useState({});

  useEffect(() => {
    axios.get(`${BASE_URL}/orders`)
      .then(response => setOrders(response.data))
      .catch(error => console.error(error));
  }, []);

  const handleClose = () => {
    setSelectedOrder(null);
    setShowModal(false);
  };

  const handleCreate = () => {
    axios.post(`${BASE_URL}/orders`, form)
      .then(() => {
        setForm({});
        setShowModal(false);
        axios.get(`${BASE_URL}/orders`)
          .then(response => setOrders(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleUpdate = () => {
    axios.put(`${BASE_URL}/orders/${selectedOrder.id}`, form)
      .then(() => {
        setSelectedOrder(null);
        setForm({});
        setShowModal(false);
        axios.get(`${BASE_URL}/orders`)
          .then(response => setOrders(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleDelete = () => {
    axios.delete(`${BASE_URL}/orders/${selectedOrder.id}`)
      .then(() => {
        setSelectedOrder(null);
        setShowModal(false);
        axios.get(`${BASE_URL}/orders`)
          .then(response => setOrders(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleSelect = (order) => {
    setSelectedOrder(order);
    setShowModal(true);
    setForm(order);
  };

  const handleChange = (event) => {
    setForm({ ...form, [event.target.name]: event.target.value });
  };

  return (
    <>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>ID</th>
            <th>User ID</th>
            <th>Order Date</th>
            <th>Ship Date</th>
            <th>Ship Address</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map((order) => (
            <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.userId}</td>
              <td>{order.orderDate}</td>
              <td>{order.shipDate}</td>
              <td>{order.shipAddress}</td>
              <td>
                <Button variant="primary" onClick={() => handleSelect(order)}>Edit</Button>{' '}
                <Button variant="danger" onClick={() => handleDelete(order)}>Delete</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showModal} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{selectedOrder ? 'Edit Order' : 'Create Order'}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formUserId">
              <Form.Label>User ID</Form.Label>
              <Form.Control type="text" name="userId" value={form.userId || ''} onChange={handleChange          } />
        </Form.Group>

        <Form.Group controlId="formOrderDate">
          <Form.Label>Order Date</Form.Label>
          <Form.Control type="text" name="orderDate" value={form.orderDate || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formShipDate">
          <Form.Label>Ship Date</Form.Label>
          <Form.Control type="text" name="shipDate" value={form.shipDate || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formShipAddress">
          <Form.Label>Ship Address</Form.Label>
          <Form.Control type="text" name="shipAddress" value={form.shipAddress || ''} onChange={handleChange} />
        </Form.Group>
      </Form>
    </Modal.Body>
    <Modal.Footer>
      <Button variant="secondary" onClick={handleClose}>
        Close
      </Button>
      {selectedOrder ? (
        <Button variant="primary" onClick={handleUpdate}>
          Save Changes
        </Button>
      ) : (
        <Button variant="primary" onClick={handleCreate}>
          Create
        </Button>
      )}
    </Modal.Footer>
  </Modal>

  <Button variant="primary" onClick={() => setShowModal(true)}>Create Order</Button>
</>
);
}

export default Orders;
