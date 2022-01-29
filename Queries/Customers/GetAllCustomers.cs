using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Queries.Customers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Domain.Customers>>
    {
    }

    internal sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Domain.Customers>>
    {
        private readonly CoreContext _coreContext;

        public GetAllCustomersQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<IEnumerable<Domain.Customers>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _coreContext.Customers.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
