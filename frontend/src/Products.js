import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Button, Modal, Form } from 'react-bootstrap';

const BASE_URL = 'http://localhost';

function Products() {
  const [products, setProducts] = useState([]);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [form, setForm] = useState({});

  useEffect(() => {
    axios.get(`${BASE_URL}/products`)
      .then(response => setProducts(response.data))
      .catch(error => console.error(error));
  }, []);

  const handleClose = () => {
    setSelectedProduct(null);
    setShowModal(false);
  };

  const handleCreate = () => {
    axios.post(`${BASE_URL}/products`, form)
      .then(() => {
        setForm({});
        setShowModal(false);
        axios.get(`${BASE_URL}/products`)
          .then(response => setProducts(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleUpdate = () => {
    axios.put(`${BASE_URL}/products/${selectedProduct.id}`, form)
      .then(() => {
        setSelectedProduct(null);
        setForm({});
        setShowModal(false);
        axios.get(`${BASE_URL}/products`)
          .then(response => setProducts(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleDelete = (selectedProduct) => {
    axios.delete(`${BASE_URL}/products/${selectedProduct.id}`)
      .then(() => {
        setSelectedProduct(null);
        setShowModal(false);
        axios.get(`${BASE_URL}/products`)
          .then(response => setProducts(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleSelect = (product) => {
    setSelectedProduct(product);
    setShowModal(true);
    setForm(product);
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
            <th>Name</th>
            <th>Description</th>
            <th>Price</th>
            <th>Owner ID</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>{product.description}</td>
              <td>{product.price}</td>
              <td>{product.ownerId}</td>
              <td>
                <Button variant="primary" onClick={() => handleSelect(product)}>Edit</Button>{' '}
                <Button variant="danger" onClick={() => handleDelete(product)}>Delete</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showModal} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{selectedProduct != null ? 'Edit Product' : 'Create Product'}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formName">
              <Form.Label>Name</Form.Label>
              <Form.Control type="text" name="name" value={form.name || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formDescription">
          <Form.Label>Description</Form.Label>
          <Form.Control type="text" name="description" value={form.description || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formPrice">
          <Form.Label>Price</Form.Label>
          <Form.Control type="text" name="price" value={form.price || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formOwnerId">
          <Form.Label>Owner ID</Form.Label>
          <Form.Control type="text" name="ownerId" value={form.ownerId || ''} onChange={handleChange} />
        </Form.Group>
      </Form>
    </Modal.Body>
    <Modal.Footer>
      <Button variant="secondary" onClick={handleClose}>
        Close
      </Button>
      {selectedProduct ? (
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

  <Button variant="primary" onClick={() => setShowModal(true)}>Create Product</Button>
</>
);
}

export default Products;
