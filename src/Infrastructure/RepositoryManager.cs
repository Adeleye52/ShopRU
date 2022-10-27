using Infrastructure.Contracts;
using Infrastructure.Data.Persistence;
using Infrastructure.Repositories;

namespace Infrastructure;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _appDbContext;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    private readonly Lazy<IDiscountRepository> _discountRepository;

    public RepositoryManager(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(appDbContext));
        _discountRepository = new Lazy<IDiscountRepository>(() => new DiscountRepository(appDbContext));


    }

    public ICustomerRepository Customer => _customerRepository.Value;
    public IDiscountRepository Discount => _discountRepository.Value;

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