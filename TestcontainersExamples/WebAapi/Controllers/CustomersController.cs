using Microsoft.AspNetCore.Mvc;
using WebAapi.Database;
using WebAapi.Entities;

namespace WebAapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    [HttpPost(Name = "PostCustomer")]
    public async Task<ActionResult> Post([FromServices]DatabaseContext context, [FromBody]Customer customer)
    {
        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();
        
        return Ok(new NewId
        {
            Id = customer.Id
        });
    }
    
    [HttpGet("{id:long}", Name = "GetCustomer")]
    public ActionResult Get([FromServices]DatabaseContext context, [FromRoute]long id)
    {
        var customer = context.Customers.FirstOrDefault(x => x.Id == id);

        if (customer is null)
            return NotFound("Not found");
        return Ok(customer);
    }
    
    [HttpGet(Name = "GetCustomers")]
    public ActionResult Get([FromServices]DatabaseContext context)
    {
        var customers = context.Customers.ToList();
        return Ok(customers);
    }
}