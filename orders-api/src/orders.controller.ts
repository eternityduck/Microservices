import { Controller, Get, Post, Body, Put, Delete } from '@nestjs/common';
import { OrdersService } from './orders.service';
import { Param } from '@nestjs/common';
import { BadRequestException } from '@nestjs/common/exceptions'
import { OrderEntity } from './entities/typeorm-order.entity';

@Controller('orders')
export class OrdersController {
  constructor(private readonly ordersService: OrdersService) {}

  @Get(':id')
  getOrder(@Param('id') id: number): Promise<OrderEntity> {
    if (typeof id === 'boolean') {
      throw new BadRequestException();
    }
    return this.ordersService.getOrder(+id);
  }

  @Get()
  getOrders(): Promise<OrderEntity[]> {
    return this.ordersService.getOrders();
  }

  @Delete(':id')
  deleteOrder(@Param('id') id: number): Promise<OrderEntity> {
    return this.ordersService.deleteOrder(+id);
  }

  @Post()
  createOrder(@Body() order: OrderEntity): Promise<OrderEntity> {
    return this.ordersService.createOrder(order);
  }

  @Put(':id')
  updateOrder(@Param('id') id: number, @Body() updateOrderDto: OrderEntity): Promise<OrderEntity> {
    return this.ordersService.updateOrder(id, updateOrderDto);
  }
}
