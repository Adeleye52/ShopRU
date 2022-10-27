namespace Infrastructure.Contracts;

public interface IRepositoryManager
{
    ICustomerRepository Customer { get; }
    IDiscountRepository Discount { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransaction(Func<Task> action);
}