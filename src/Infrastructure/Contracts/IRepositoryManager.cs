namespace Infrastructure.Contracts;

public interface IRepositoryManager
{
    ICustomerRepository Customer { get; }
    ICouponRepository Coupon { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransaction(Func<Task> action);
}