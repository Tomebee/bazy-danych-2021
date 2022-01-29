using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Queries.Orders
{
    public class GetOrdersFromThisMonthQuery : IRequest<IEnumerable<Domain.Orders>>
    {

    }

    internal sealed class GetOrdersFromThisMonthQueryHandler : IRequestHandler<GetOrdersFromThisMonthQuery, IEnumerable<Domain.Orders>>
    {
        private readonly CoreContext _coreContext;

        public GetOrdersFromThisMonthQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<IEnumerable<Domain.Orders>> Handle(GetOrdersFromThisMonthQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            return (await _coreContext.Orders.ToListAsync(cancellationToken: cancellationToken)).Where(x =>
                x.OrderDate.HasValue && x.OrderDate.Value.Year == now.Year && x.OrderDate.Value.Month == now.Month);
        }
    }
}
