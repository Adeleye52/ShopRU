using Application.Contracts;
using Application.DataTransferObjects;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private  IRepositoryManager _repository { get; set; }
        private  IMapper _mapper { get; set; }

        public InvoiceService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SuccessResponse<InvoiceDto>> GetInvoice(Guid customerId,string productType,  decimal bill)
        {
            var customer = await _repository.Customer.FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
                throw new RestException(HttpStatusCode.NotFound, "Customer not found");
            var discount = 0m;
            decimal discountPercentage = 0m;
            if (productType.ToLower() != EPruductType.Groceries.ToString().ToLower())
            {
                discountPercentage = await GetDiscount(customer);
                discount = GetDiscountOnCustomer(bill, discountPercentage);
                bill -= discount;
            }
            var priceDiscount = await GetDiscountOnProductPrice(bill);
            var totalbill = bill - priceDiscount;

            InvoiceDto invoice = new ()
            {

            }


            throw new NotImplementedException();
        }
        private async Task<decimal> GetDiscount(Customer customer)
        {
            decimal discountPercentage = 0m;
            Discount discount = null;
            if (customer.Type == ECustomerType.Employee.ToString())
            {
                discount = await _repository.Coupon.FirstOrDefaultAsync(x => x.Type == EDiscountType.Employee.ToString());
                discountPercentage = discount?.Percentage ?? 0m;
               
            }
            else if (customer.Type == ECustomerType.Affilate.ToString())
            {
                discount = await _repository.Coupon.FirstOrDefaultAsync(x => x.Type == EDiscountType.Employee.ToString());
                discountPercentage = discount?.Percentage ?? 0m;


            }
            else if (customer.CreatedAt.AddYears(2) > DateTime.UtcNow)
            {
                discount = await _repository.Coupon.FirstOrDefaultAsync(x => x.Type == EDiscountType.Employee.ToString());
                discountPercentage = discount?.Percentage ?? 0m;


            }
            
            return discountPercentage;
        }
        private decimal GetDiscountOnCustomer(decimal totalPrice, decimal discount)
        {

            var discountVal = discount / 100;
            
            return totalPrice * discountVal;
        }
        private async Task<decimal> GetDiscountOnProductPrice(decimal totalPrice)
        {
            var discount = await _repository.Coupon.FirstOrDefaultAsync(x=>x.Type == EDiscountType.ProductPrice.ToString());
            var discountVal = discount.Percentage / 100;
            var total = totalPrice - (totalPrice % 100);
           
            return total * discountVal;
        }
    }
}
