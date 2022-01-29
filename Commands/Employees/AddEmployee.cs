using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Domain;

namespace Northwind.Commands.Employees
{
    public class AddEmployeeQuery : IRequest<Domain.Employees>
    {
        public Domain.Employees Employee { get; set; }
    }

    internal sealed class AddEmployeeQueryHandler : IRequestHandler<AddEmployeeQuery, Domain.Employees>
    {
        private readonly CoreContext _coreContext;

        public AddEmployeeQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<Domain.Employees> Handle(AddEmployeeQuery request, CancellationToken cancellationToken)
        {
            var entry = await _coreContext.Set<Domain.Employees>().AddAsync(request.Employee, cancellationToken);

            await _coreContext.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
    }
}
