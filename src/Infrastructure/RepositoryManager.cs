using Infrastructure.Contracts;
using Infrastructure.Data.DbContext;
using Infrastructure.Repositories;

namespace Infrastructure;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _appDbContext;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    private readonly Lazy<ICouponRepository> _couponRepository;

    public RepositoryManager(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(appDbContext));
        _couponRepository = new Lazy<ICouponRepository>(() => new CouponRepository(appDbContext));


    }

    public ICustomerRepository Customer => _customerRepository.Value;
    public ICouponRepository Coupon => _couponRepository.Value;

    public async Task<int> SaveChangesAsync() => await _appDbContext.SaveChangesAsync();
    public async Task BeginTransaction(Func<Task> action)
    {
        await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            await action();

            await SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}