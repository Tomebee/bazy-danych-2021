using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Queries.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<Domain.Orders>>
    {
        public int Skip = 0;
        public int Take = 10;
        public bool? Shipped = null;
    }

    internal sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Domain.Orders>>
    {
        private readonly CoreContext _coreContext;

        public GetOrdersQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<IEnumerable<Domain.Orders>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = (await _coreContext.Orders.ToListAsync(cancellationToken: cancellationToken)).Skip(request.Skip).Take(request.Take)
                .Where(x => request.Shipped is null || x.ShippedDate.HasValue);
            return orders;
        }
    }
}
