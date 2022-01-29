using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Queries.Employees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<Domain.Employees>>
    {
    }

    internal sealed class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Domain.Employees>>
    {
        private readonly CoreContext _coreContext;

        public GetAllEmployeesQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<IEnumerable<Domain.Employees>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _coreContext.Employees.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
