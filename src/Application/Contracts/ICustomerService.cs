using Application.DataTransferObjects;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICustomerService
    {
        Task<SuccessResponse<CustomerDto>> Create(CustomerCreateDto model);
        Task<SuccessResponse<CustomerDto>> GetById(Guid id);
        Task<SuccessResponse<CustomerDto>> GetByName(string username);
        Task<PagedResponse<IEnumerable<CustomerDto>>> GetAll(ResourceParameter parameter, string name, IUrlHelper urlHelper);
    }
}
