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
        Task<SuccessResponse<CouponDto>> AddDiscount(CouponCreateDto model);
        Task<SuccessResponse<CouponDto>> GetByType(string type);
        Task<PagedResponse<IEnumerable<CouponDto>>> GetAll(ResourceParameter parameter, string name, IUrlHelper urlHelper);

    }
}
