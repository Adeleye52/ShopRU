using Domain.Entities;
using Infrastructure.Contracts;
using Infrastructure.Data.Persistence;


namespace Infrastructure.Repositories
{
    internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {

        }
    }
}
