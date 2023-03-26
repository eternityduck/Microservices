import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm'
import { Repository } from 'typeorm'
import { OrderEntity } from './entities/typeorm-order.entity';

@Injectable()
export class OrdersService {

  constructor(
    @InjectRepository(OrderEntity)
    private ordersRepository: Repository<OrderEntity>,
  ){}

  getOrders(): Promise<OrderEntity[]> {
    return this.ordersRepository.find();
  }

  getOrder(id: number): Promise<OrderEntity | null> {
    return this.ordersRepository.findOneBy({ id });
  }

  createOrder(order: OrderEntity): Promise<OrderEntity> {
    const newOrder = this.ordersRepository.create(order);
    return this.ordersRepository.save(newOrder);
  }

  async updateOrder(id: number, order: OrderEntity): Promise<OrderEntity>{
    await this.ordersRepository.update(id, order);
    const updatedOrder = await this.getOrder(id);
    return updatedOrder;
  }
  
  async deleteOrder(id: number): Promise<OrderEntity> {
    let order = await this.getOrder(id);
    return this.ordersRepository.remove(order);
  }
}
