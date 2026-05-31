using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce.Repositories
{
    public class SkusRepo : BaseRepo<StockKeepingUnit, Guid>, IRepoWrite<StockKeepingUnit>, IRepoRead<Guid, StockKeepingUnit>
    {
        public SkusRepo(EcommerceDbContext context) : base(context)
        {
        }
    }
}
