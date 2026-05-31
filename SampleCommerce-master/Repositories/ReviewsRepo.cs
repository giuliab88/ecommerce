using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce.Repositories
{
    public class ReviewsRepo : BaseRepo<Review, int>, IRepoWrite<Review>, IRepoRead<int, Review>
    {
        public ReviewsRepo(EcommerceDbContext context) : base(context)
        {
        }
    }
}
