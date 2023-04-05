import React, { useState } from 'react';
import { Button } from 'react-bootstrap';
import Orders from './Orders';
import Products from './Products';
import Users from './Users';

function App() {
  const [activeComponent, setActiveComponent] = useState('Orders');

  const renderComponent = () => {
    switch (activeComponent) {
      case 'Orders':
        return <Orders />;
      case 'Products':
        return <Products />;
      case 'Users':
        return <Users />;
      default:
        return <Orders />;
    }
  };

  return (
    <div>
      <div class="ModeSelector">
        <Button variant="primary" onClick={() => setActiveComponent('Orders')}>Orders</Button>
        <Button variant="primary" onClick={() => setActiveComponent('Products')}>Products</Button>
        <Button variant="primary" onClick={() => setActiveComponent('Users')}>Users</Button>
      </div>
      <div class="MainDisplay">
          {renderComponent()}
      </div>
    </div>
  );
}

export default App;
