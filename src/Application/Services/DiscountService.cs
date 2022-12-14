using Application.Contracts;
using Application.DataTransferObjects;
using Application.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DiscountService : IDiscountService
    {
        private IRepositoryManager _repository { get; set; }
        private IMapper _mapper { get; set; }

        public DiscountService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SuccessResponse<DiscountDto>> AddDiscount(DiscountCreateDto model)
        {
            var discountExists = await _repository.Discount.ExistsAsync(x => x.Type.ToLower() == model.Type.ToLower());
            if (discountExists)
                throw new RestException(HttpStatusCode.BadRequest, "A discount with this type already exist");
            var discount = _mapper.Map<Discount>(model);
            await _repository.Discount.AddAsync(discount);
            await _repository.SaveChangesAsync();
            var response = _mapper.Map<DiscountDto>(discount);
            return new SuccessResponse<DiscountDto>
            {
                Success = true,
                Message = "Data created successfully",
                Data = response
            };
        }

        public async Task<PagedResponse<IEnumerable<DiscountDto>>> GetAll(ResourceParameter parameter, string name, IUrlHelper urlHelper)
        {

            var query = _repository.Discount.QueryAll();

            if (!string.IsNullOrEmpty(parameter.Search))
            {
                var search = parameter.Search.ToLower();
                var searchQuery = parameter.Search.ToLower().Trim();
                query = query.Where(x =>
                      (x.Type.ToLower().Contains(searchQuery)));
            }

            query = query.OrderByDescending(x => x.CreatedAt);

            var projectedQuery = query.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider);

            var discounts = await PagedList<DiscountDto>.Create(projectedQuery, parameter.PageNumber, parameter.PageSize, parameter.Sort);
            var page = PageUtility<DiscountDto>.CreateResourcePageUrl(parameter, name, discounts, urlHelper);

            return new PagedResponse<IEnumerable<DiscountDto>>
            {
                Message = "Data retrieved successfully",
                Data = discounts,
                Meta = new Meta
                {
                    Pagination = page
                }
            };
        }

        public async Task<SuccessResponse<DiscountDto>> GetByType(string type)
        {
            var discount = await _repository.Discount.FirstOrDefaultAsync(x => x.Type.ToLower() == type.ToLower());
            if (discount == null)
                throw new RestException(HttpStatusCode.NotFound, "Discount not found");
            var response = _mapper.Map<DiscountDto>(discount);
            return new SuccessResponse<DiscountDto>
            {
                Message = "Data retrieved successfully",
                Data = response,
                Success = true
            };
        }
    }
}
