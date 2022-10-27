

using Domain.Entities;
using Infrastructure.Contracts;
using Infrastructure.Data.Persistence;

namespace Infrastructure.Repositories
{
    public class DiscountRepository:RepositoryBase<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {

        }
    }
}
