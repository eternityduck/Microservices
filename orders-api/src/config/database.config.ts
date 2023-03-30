import { registerAs } from '@nestjs/config'
import { OrderEntity } from 'src/entities/typeorm-order.entity'

export const databaseConfig = registerAs('database', () => ({
  type: 'postgres',
  host: process.env.POSTGRES_HOST,
  port: process.env.POSTGRES_PORT || 5432,
  username: process.env.POSTGRES_USER,
  password: process.env.POSTGRES_PASSWORD,
  database: process.env.POSTGRES_NAME,
  entities: [OrderEntity],
  migrations: ['src/migrations/*.{js,ts}'],
}));

export * from './database.config';
