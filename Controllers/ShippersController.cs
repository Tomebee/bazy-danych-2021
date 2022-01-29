using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Queries.Shippers;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/shippers")]
    public class ShippersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShippersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllShippersQuery()));
    }
}
