using Application.DataTransferObjects;
using Application.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IDiscountService
    {
        Task<SuccessResponse<DiscountDto>> AddDiscount(DiscountCreateDto model);
        Task<SuccessResponse<DiscountDto>> GetByType(string type);
        Task<PagedResponse<IEnumerable<DiscountDto>>> GetAll(ResourceParameter parameter, string name, IUrlHelper urlHelper);

    }
}
