import axios from "axios";

export default class OrdersService {
  static async getAll() {
    try {
      const apiURL = "/orders";
      const response = await axios.get(apiURL);
      return response.data;
    } catch (e) {
      console.log(e);
    }
  }

  static async deleteOrder(id) {
    await axios.delete(`/orders/` + id);
  }

  static async updateOrder(editedOrder) {
    const body = {
      id: editedOrder.id,
      userId: editedOrder.userId,
      shipAddress: editedOrder.shipAddress,
      shipDate: editedOrder.shipDate,
    };
    await axios.put(`/orders`, body);
  }

  static async createOrder(newOrder) {
    const body = {
      userId: newOrder.userId,
      shipAddress: newOrder.shipAddress,
      shipDate: newOrder.shipDate,
    };
    const response = axios.post(`/orders`, body);
    return response.data;
  }
}
