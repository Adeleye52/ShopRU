using Application.DataTransferObjects;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class CustomerMapper :Profile
    {
        public CustomerMapper()
        {
            CreateMap<CustomerCreateDto, Customer>().AfterMap((src, dest) =>
            {
                dest.UserName = src.UserName?.Trim();
                dest.EmailAddress = src.EmailAddress?.ToLower();

            });
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
