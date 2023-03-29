import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm'

@Entity ("orders")
export class OrderEntity{
  @PrimaryGeneratedColumn('increment')
  public id: number;

  @Column()
  public userId: number;

  @Column()
  public orderDate: Date;

  @Column()
  public shipDate: Date;

  @Column()
  public shipAddress: string;

  @Column()
  public status: string;
}
