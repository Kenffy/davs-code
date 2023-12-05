using Microsoft.EntityFrameworkCore;
using Orders.Data;
using Orders.Models;
using Orders.Repository.IRepository;

namespace Orders.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _context;

        public OrderRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(string id)
        {
            var entity = _context.Orders.Find(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task UpdateOrderAsync(Order entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
