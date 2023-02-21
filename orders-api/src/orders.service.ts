import { Injectable } from '@nestjs/common';
import {Order} from './data/order';

@Injectable()
export class OrdersService {
  private readonly orders: Order[] = [
    new Order(1, 1, new Date(), new Date(), '123 Main St'),
    new Order(2, 1, new Date(), new Date(), '123 Main St'),
    new Order(3, 1, new Date(), new Date(), '123 Main St'),
    new Order(4, 1, new Date(), new Date(), '123 Main St'),
    new Order(5, 1, new Date(), new Date(), '123 Main St'),
    new Order(6, 1, new Date(), new Date(), '123 Main St'),
  ]

  getOrders(): Order[] {
    return this.orders;
  }
  getOrder(id: number): Order {
    return this.orders.find(order => order.id === id);
  }
  createOrder(order: Order): Order[] {
    this.orders.push(order);
    return this.orders;
  }
  updateOrder(id: number, order: Order): Order{
    const index = this.orders.findIndex(order => order.id === id);
    this.orders[index] = order;
    return this.orders[index];
  }
}
