using Application.Contracts;
using Application.DataTransferObjects;
using Application.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        public IRepositoryManager _repository { get; set; }
        public IMapper _mapper { get; set; }

        public CustomerService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SuccessResponse<CustomerDto>> Create(CustomerCreateDto model)
        {
            var customerExists = await _repository.Customer.ExistsAsync(x => x.UserName.ToLower() == model.UserName.ToLower()
            || x.EmailAddress.ToLower() == model.EmailAddress.ToLower());
            if (customerExists)
                throw new RestException(HttpStatusCode.BadRequest, "Customer already exist");
            var customer = _mapper.Map<Customer>(model);
            await _repository.Customer.AddAsync(customer);
            await _repository.SaveChangesAsync();
            var response = _mapper.Map<CustomerDto>(customer);
            return new SuccessResponse<CustomerDto>
            {
                Success = true,
                Message = "Data created successfully",
                Data = response
        };
        }

        public async Task<PagedResponse<IEnumerable<CustomerDto>>> GetAll(ResourceParameter parameter, string name, IUrlHelper urlHelper)
        {
            var query = _repository.Customer.QueryAll();

            if (!string.IsNullOrEmpty(parameter.Search))
            {
                var search = parameter.Search.ToLower();
                var searchQuery = parameter.Search.ToLower().Trim();
                query = query.Where(x =>
                      (x.UserName.ToLower().Contains(searchQuery))
                     || (!string.IsNullOrWhiteSpace(x.FirstName) && x.FirstName.ToLower().Contains(searchQuery))
                     || (!string.IsNullOrWhiteSpace(x.LastName) && x.LastName.ToLower().Contains(searchQuery))
                     || (!string.IsNullOrWhiteSpace(x.Type) && x.Type.ToLower().Contains(searchQuery)));
            }

            query = query.OrderByDescending(x => x.CreatedAt);

            var projectedQuery = query.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);

            var customers = await PagedList<CustomerDto>.Create(projectedQuery, parameter.PageNumber, parameter.PageSize, parameter.Sort);
            var page = PageUtility<CustomerDto>.CreateResourcePageUrl(parameter, name, customers, urlHelper);

            return new PagedResponse<IEnumerable<CustomerDto>>
            {
                Message = "Data retrieved successfully",
                Data = customers,
                Meta = new Meta
                {
                    Pagination = page
                }
            };
        }

        public async Task<SuccessResponse<CustomerDto>> GetById(Guid id)
        {
            var customer = await _repository.Customer.GetByIdAsync(id);
            if (customer == null)
                throw new RestException(HttpStatusCode.NotFound, "Customer not found");
            var response = _mapper.Map<CustomerDto>(customer);
            return new SuccessResponse<CustomerDto>
            {
                Message = "Data retrieved successfully",
                Data = response,
                Success = true
            };
        }

        public async Task<SuccessResponse<CustomerDto>> GetByName(string username)
        {
            var customer = await _repository.Customer.FirstOrDefaultAsync(x=>x.UserName.ToLower() == username.ToLower());
            if (customer == null)
                throw new RestException(HttpStatusCode.NotFound, "Customer not found");
            var response = _mapper.Map<CustomerDto>(customer);
            return new SuccessResponse<CustomerDto>
            {
                Message = "Data retrieved successfully",
                Data = response,
                Success = true
            };
        }
    }
}
