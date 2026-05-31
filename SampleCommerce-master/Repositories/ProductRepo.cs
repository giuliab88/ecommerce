using Microsoft.EntityFrameworkCore;
using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce.Repositories
{
    public class ProductRepo : BaseRepo<Product, Guid>, IRepoWrite<Product>, IRepoRead<Guid, Product>
    {
        public ProductRepo(EcommerceDbContext context) : base(context) { }

        public override Product? GetById(Guid id)
        {
            return _context.Products
                .Include(p => p.StockKeepingUnits)
                .FirstOrDefault(p => p.Id == id);
        }

        public override List<Product> GetAll()
        {
            return _context.Products
                .AsNoTracking()
                .Include(p => p.StockKeepingUnits)
                .ToList();
        }
    }
}
