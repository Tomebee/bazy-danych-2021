using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Domain;
using Northwind.Model;

namespace Northwind.Commands.Orders
{
    public class CreateOrderQuery : IRequest<Domain.Orders>
    {
        public string CustomerId { get; set; }
        public OrderDto OrderDto { get; set; }
    }

    internal sealed class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, Domain.Orders>
    {
        private readonly CoreContext _coreContext;

        public CreateOrderQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<Domain.Orders> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
        {
            var product = await _coreContext.Products.FindAsync(request.OrderDto.OrderDetails.ProductId.Value);

            var order = new Domain.Orders()
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                ShipAddress = request.OrderDto.ShipAddress,
                ShipCity = request.OrderDto.ShipCity,
                ShipCountry = request.OrderDto.ShipCountry,
                ShipName = request.OrderDto.ShipName,
                ShipPostalCode = request.OrderDto.ShipPostalCode,
                ShipRegion = request.OrderDto.ShipRegion,
                ShipVia = request.OrderDto.ShipVia
            };

            var entry = await _coreContext.Set<Domain.Orders>().AddAsync(order, cancellationToken);

            await _coreContext.SaveChangesAsync(cancellationToken);

            var details = new OrderDetails()
            {
                ProductId = product.ProductId,
                Quantity = request.OrderDto.OrderDetails.Quantity,
                OrderId = entry.Entity.OrderId,
                UnitPrice = product.UnitPrice
            };

            await _coreContext.AddAsync(details, cancellationToken);

            await _coreContext.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
    }
}
