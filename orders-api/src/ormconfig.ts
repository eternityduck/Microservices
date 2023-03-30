import { DataSource } from 'typeorm';
import { OrderEntity } from './entities/typeorm-order.entity'
import * as dotenv from 'dotenv'

dotenv.config()

const dataSource = new DataSource({
  type: 'postgres',
  host: process.env.POSTGRES_HOST,
  port: +process.env.POSTGRES_PORT || 5432,
  username: process.env.POSTGRES_USER,
  password: process.env.POSTGRES_PASSWORD,
  database: process.env.POSTGRES_NAME,
  entities: [OrderEntity],
  migrations: ['src/migrations/*.{js,ts}'],
});

export default dataSource;
