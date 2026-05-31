using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce.Repositories
{
    public class OrdersRepo : BaseRepo<Order, int>, IRepoWrite<Order>, IRepoRead<int, Order>
    {
        public OrdersRepo(EcommerceDbContext context) : base(context) { }

        public List<Order>? GetAllByUser(Guid userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}
