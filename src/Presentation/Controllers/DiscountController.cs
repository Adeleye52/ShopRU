using Application.Contracts;
using Application.DataTransferObjects;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/discount")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IServiceManager _service;

    public DiscountController(IServiceManager service)
    {
        _service = service;
    }


    /// <summary>
    /// add a new discount
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<DiscountDto>), 200)]
    public async Task<IActionResult> CreateDiscount([FromBody] DiscountCreateDto model)
    {
        var result = await _service.DiscountService.AddDiscount(model);
        return Ok(result);
    }

    /// <summary>
    /// get discont by id
    /// </summary>
    /// <param name="type"></param>
    /// <returns>New User</returns>
    [HttpGet("type")]
    [ProducesResponseType(typeof(SuccessResponse<DiscountDto>), 200)]
    public async Task<IActionResult> GetDiscountById([FromRoute] string type)
    {
        var result = await _service.DiscountService.GetByType(type);
        return Ok(result);
    }

    
    /// <summary>
    /// get all customers
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns>New User</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<DiscountDto>), 200)]
    public async Task<IActionResult> GetDiscounts([FromQuery] ResourceParameter parameter)
    {
        var result = await _service.DiscountService.GetAll(parameter, nameof(GetDiscounts), Url);
        return Ok(result);
    }

}