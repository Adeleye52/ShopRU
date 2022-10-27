

using Domain.Entities;
using Infrastructure.Contracts;
using Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class CouponRepository:RepositoryBase<Discount>, ICouponRepository
    {
        public CouponRepository(AppDbContext context) : base(context)
        {

        }
    }
}
