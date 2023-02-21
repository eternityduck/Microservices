import { Controller, Get, Post, Body, Put } from '@nestjs/common';
import { OrdersService } from './orders.service';
import { Order } from './data/order';
import { Param } from '@nestjs/common';
import { BadRequestException } from '@nestjs/common/exceptions'
import { CreateOrderDto } from './dto/createOrderDto'
import { UpdateOrderDto } from './dto/updateOrderDto'

@Controller('orders')
export class OrdersController {
  constructor(private readonly ordersService: OrdersService) {}

  @Get(':id')
  getOrder(@Param('id') id: number): Order {
    if (typeof id === 'boolean') {
      throw new BadRequestException();
    }
    return this.ordersService.getOrder(+id);
  }

  @Get()
  getOrders(): Order[] {
    return this.ordersService.getOrders();
  }

  @Post()
  createOrder(@Body() order: Order): Order[] {
    return this.ordersService.createOrder(order);
  }

  @Put(':id')
  update(@Param('id') id: number, @Body() updateOrderDto: Order) {
    return this.ordersService.updateOrder(id, updateOrderDto);
  }
}
