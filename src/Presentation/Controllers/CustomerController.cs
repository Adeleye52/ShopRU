using Application.Contracts;
using Application.DataTransferObjects;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Presentation.Controllers;

[Route("api/v1/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IServiceManager _service;

    public CustomerController(IServiceManager service)
    {
        _service = service;
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<CustomerDto>), 200)]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto model)
    {
        var result = await _service.CustomerService.Create(model);
        return Ok(result);
    }

    /// <summary>
    /// get customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>New User</returns>
    [HttpGet("id")]
    [ProducesResponseType(typeof(SuccessResponse<CustomerDto>), 200)]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var result = await _service.CustomerService.GetById(id);
        return Ok(result);
    }

    /// <summary>
    /// Get customer by username
    /// </summary>
    /// <param name="username"></param>
    /// <returns>New User</returns>
    [HttpGet("name")]
    [ProducesResponseType(typeof(SuccessResponse<CustomerDto>), 200)]
    public async Task<IActionResult> RegisterUser( string username)
    {
        var result = await _service.CustomerService.GetByName(username);
        return Ok(result);
    }

    /// <summary>
    /// get all customers
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns>New User</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<CustomerDto>), 200)]
    public async Task<IActionResult> GetCustomers([FromQuery] ResourceParameter parameter)
    {
        var result = await _service.CustomerService.GetAll(parameter, nameof(GetCustomers), Url);
        return Ok(result);
    }

    
}