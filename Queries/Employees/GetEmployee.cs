using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Domain;

namespace Northwind.Queries.Employees
{
    public class GetEmployeeQuery : IRequest<Domain.Employees>
    {
        public int Id { get; set; }
    }

    internal sealed class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery,Domain.Employees>
    {
        private readonly CoreContext _coreContext;

        public GetEmployeeQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<Domain.Employees> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _coreContext.Employees.FindAsync(request.Id);
        }
    }
}
