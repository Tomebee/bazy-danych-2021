using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Commands.Orders;
using Northwind.Domain;
using Northwind.Model;
using Northwind.Queries.Orders;

namespace Northwind.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CoreContext _publicContext;
        private readonly IMediator _mediator;

        public OrdersController(CoreContext publicContext, IMediator mediator)
        {
            _publicContext = publicContext;
            _mediator = mediator;
        }

        [HttpGet("api/orders/recent")]
        public async Task<IActionResult> OrdersFromThisMonth()
        {
            return Ok(await _mediator.Send(new GetOrdersFromThisMonthQuery()));
        }

        [HttpGet]
        [Route("api/orders")]
        public async Task<IActionResult> GetOrders([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] bool? shipped = null)
        {
            var orders = await _mediator.Send(new GetOrdersQuery
            {
                Skip = skip,
                Take = take,
                Shipped = shipped
            });

            return Ok(orders);
        }

        [HttpPost("api/customers/{id}/orders")]
        public async Task<IActionResult> CreateNew([FromRoute] string id, OrderDto orderDto)
        {
            if (await _publicContext.Customers.FindAsync(id) is null)
            {
                return NotFound(new
                {
                    Error = "Customer not found"
                });
            }

            var product = await _publicContext.Products.FindAsync(orderDto.OrderDetails.ProductId.Value);

            if (product.UnitsInStock < orderDto.OrderDetails.Quantity)
            {
                return BadRequest(new
                {
                    Error = "Invalid order quantity"
                });
            }

            var order = await _mediator.Send(new CreateOrderQuery
            {
                OrderDto = orderDto,
                CustomerId = id
            });

            return Ok(order);
        }
    }
}
