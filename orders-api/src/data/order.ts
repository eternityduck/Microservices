export class Order{
  constructor(public id: number, public userId: number, public orderDate: Date, public shipDate: Date, public shipAddress: string) {}
}
