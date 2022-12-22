using Microsoft.AspNetCore.Mvc;
using WebAapi.Database;

namespace WebAapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/{id:long}", Name = "GetCustomer")]
    public ActionResult Get([FromServices]DatabaseContext context, [FromRoute]long id)
    {
        var customer = context.Customers.FirstOrDefault(x => x.Id == id);

        if (customer is null)
            return NotFound("Not found");
        return Ok(customer);
    }
}