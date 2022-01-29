using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Queries.Shippers
{
    public class GetAllShippersQuery : IRequest<IEnumerable<Domain.Shippers>>
    {
    }

    internal sealed class GetAllShippersQueryHandler : IRequestHandler<GetAllShippersQuery, IEnumerable<Domain.Shippers>>
    {
        private readonly CoreContext _coreContext;

        public GetAllShippersQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<IEnumerable<Domain.Shippers>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
        {
            return await _coreContext.Shippers.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
