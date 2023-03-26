import { Module } from '@nestjs/common';
import { OrdersController } from './orders.controller';
import { OrdersService } from './orders.service';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { OrderEntity } from './entities/typeorm-order.entity'
import { ordersTable1679940251693 } from './migrations/1679940251693-orders-table'

@Module({
  imports: [
    TypeOrmModule.forRootAsync({
      imports: [ConfigModule],
      useFactory: async (config: ConfigService) => ({
        type: 'postgres',
        host: 'postgres-orders',
        port: 5432,
        database: 'orders',
        username: 'postgres',
        password: 'admin',
        entities: [OrderEntity],
        autoLoadEntities: true,
        synchronize: false,
        migrations: [ordersTable1679940251693],
        migrationsRun: true,
      }),
      inject: [ConfigService],
    }),
    TypeOrmModule.forFeature([OrderEntity]),
  ],
  controllers: [OrdersController],
  providers: [OrdersService],
})
export class AppModule {}
