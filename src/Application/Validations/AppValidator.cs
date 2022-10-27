using Application.DataTransferObjects;

using Domain.Enums;
using FluentValidation;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Application.Validations
{
    public class CustomerValidator:AbstractValidator<CustomerCreateDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Enter a Valid Email Address");
            RuleFor(x => x.Type).IsEnumName(typeof(ECustomerType), caseSensitive: false).WithMessage("This value is not a valid Customer type. Please selecect either Affiliate, Employee or Other"); 
         
        }
    }
    public class DiscountValidator : AbstractValidator<DiscountCreateDto>
    {
        public DiscountValidator()
        {
            RuleFor(x => x.Percentage).GreaterThan(0);

        }
    }
   
}
