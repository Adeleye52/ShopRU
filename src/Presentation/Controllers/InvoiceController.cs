using Application.Contracts;
using Application.DataTransferObjects;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/invoice")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IServiceManager _service;

    public InvoiceController(IServiceManager service)
    {
        _service = service;
    }


    /// <summary>
    /// get invoice
    /// </summary>
    /// <param name="model"></param>
    /// <returns>New User</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<InvoiceDto>), 200)]
    public async Task<IActionResult> GetCustomers([FromQuery] GetInvoiceDto model)
    {
        var result = await _service.InvoiceService.GetInvoice(model);
        return Ok(result);
    }

}