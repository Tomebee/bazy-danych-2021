using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Queries.Products
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Domain.Products>>
    {
    }

    internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Domain.Products>>
    {
        private readonly CoreContext _coreContext;

        public GetAllProductsQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<IEnumerable<Domain.Products>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _coreContext.Products.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
