using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Commands.Customers;
using Northwind.Domain;
using Northwind.Queries.Customers;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllCustomersQuery()));

        [HttpPost]
        public async Task<IActionResult> PostNew(Customers customer)
        {
            return Ok(await _mediator.Send(new AddCustomerQuery
            {
                Customer = customer
            }));
        }
    }
}
