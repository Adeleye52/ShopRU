using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ServiceManager : IServiceManager
{
   // private readonly Lazy<ICompanyService> _companyService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, 
        UserManager<User> userManager, IConfiguration configuration)
    {
        //_companyService = 
        //    new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, logger, mapper));
        
    }
    
   // public ICompanyService CompanyService => _companyService.Value;
  
}