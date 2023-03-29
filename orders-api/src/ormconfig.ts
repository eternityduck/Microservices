import { DataSource } from 'typeorm';
import { OrderEntity } from './entities/typeorm-order.entity';

const dataSource = new DataSource({
  type: 'postgres',
  host: 'localhost',
  port: 5432,
  username: 'postgres',
  password: 'admin',
  database: 'orders',
  entities: [OrderEntity],
  migrations: ['src/migrations/*.{js,ts}'],
});

export default dataSource;
