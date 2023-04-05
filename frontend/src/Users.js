import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Button, Modal, Form } from 'react-bootstrap';

const BASE_URL = 'http://localhost';

function Users() {
  const [users, setUsers] = useState([]);
  const [selectedUser, setSelectedUser] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [form, setForm] = useState({});

  useEffect(() => {
    axios.get(`${BASE_URL}/users`)
      .then(response => setUsers(response.data))
      .catch(error => console.error(error));
  }, []);

  const handleClose = () => {
    setSelectedUser(null);
    setShowModal(false);
  };

  const handleCreate = () => {
    axios.post(`${BASE_URL}/users`, form)
      .then(() => {
        setForm({});
        setShowModal(false);
        axios.get(`${BASE_URL}/users`)
          .then(response => setUsers(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleUpdate = () => {
    axios.put(`${BASE_URL}/users/${selectedUser.id}`, form)
      .then(() => {
        setSelectedUser(null);
        setForm({});
        setShowModal(false);
        axios.get(`${BASE_URL}/users`)
          .then(response => setUsers(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleDelete = (selectedUser) => {
    axios.delete(`${BASE_URL}/users/${selectedUser.id}`)
      .then(() => {
        setSelectedUser(null);
        setShowModal(false);
        axios.get(`${BASE_URL}/users`)
          .then(response => setUsers(response.data))
          .catch(error => console.error(error));
      })
      .catch(error => console.error(error));
  };

  const handleSelect = (user) => {
    setSelectedUser(user);
    setShowModal(true);
    setForm(user);
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
            <th>First name</th>
            <th>Last name</th>
            <th>Phone number</th>
            <th>Email</th>
            <th>Birthday</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>{user.phoneNumber}</td>
              <td>{user.email}</td>
              <td>{user.birthDay}</td>
              <td>
                <Button variant="primary" onClick={() => handleSelect(user)}>Edit</Button>{' '}
                <Button variant="danger" onClick={() => handleDelete(user)}>Delete</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showModal} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{selectedUser ? 'Edit User' : 'Create User'}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formFirstName">
              <Form.Label>First name</Form.Label>
              <Form.Control type="text" name="firstName" value={form.firstName || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formLastName">
              <Form.Label>Last name</Form.Label>
              <Form.Control type="text" name="lastName" value={form.lastName || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formPhoneNumber">
          <Form.Label>Phone number</Form.Label>
          <Form.Control type="text" name="phoneNumber" value={form.phoneNumber || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formEmail">
          <Form.Label>Email</Form.Label>
          <Form.Control type="text" name="email" value={form.email || ''} onChange={handleChange} />
        </Form.Group>

        <Form.Group controlId="formBirthDay">
          <Form.Label>Birthday</Form.Label>
          <Form.Control type="text" name="birthDay" value={form.birthDay || ''} onChange={handleChange} />
        </Form.Group>
      </Form>
    </Modal.Body>
    <Modal.Footer>
      <Button variant="secondary" onClick={handleClose}>
        Close
      </Button>
      {selectedUser ? (
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

  <Button variant="primary" onClick={() => setShowModal(true)}>Create User</Button>
</>
);
}

export default Users;
