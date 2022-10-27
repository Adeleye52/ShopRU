using Application.DataTransferObjects;

namespace Application.Contracts;

public interface IServiceManager
{
    IDiscountService DiscountService { get; }
    ICustomerService CustomerService { get; }
    IInvoiceService InvoiceService { get; }
}