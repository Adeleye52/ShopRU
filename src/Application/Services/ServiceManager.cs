using Application.Contracts;
using AutoMapper;
using Infrastructure.Contracts;

namespace Application.Services;

public class ServiceManager : IServiceManager
{
   private readonly Lazy<IDiscountService> _discountService;
   private readonly Lazy<ICustomerService> _customerService;
   private readonly Lazy<IInvoiceService> _invoiceService;
    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _discountService =
            new Lazy<IDiscountService>(() => new DiscountService(repositoryManager, mapper));
        _customerService =
            new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, mapper));
        _invoiceService =
            new Lazy<IInvoiceService>(() => new InvoiceService(repositoryManager, mapper));

    }

     public IDiscountService DiscountService => _discountService.Value;
     public ICustomerService CustomerService => _customerService.Value;
     public IInvoiceService InvoiceService => _invoiceService.Value;

}