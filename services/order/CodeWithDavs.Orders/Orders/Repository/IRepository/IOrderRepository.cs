using Orders.Models;

namespace Orders.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order entity);
        Task UpdateOrderAsync(Order entity);
        Task DeleteOrderAsync(string id);
        Task<Order> GetOrderAsync(string id);
        Task<IReadOnlyList<Order>> GetOrdersAsync();
    }
}
