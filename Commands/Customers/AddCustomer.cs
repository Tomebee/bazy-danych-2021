using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Domain;

namespace Northwind.Commands.Customers
{
    public class AddCustomerQuery : IRequest<Domain.Customers>
    {
        public Domain.Customers Customer { get; set; }
    }

    internal sealed class AddCustomerQueryHandler : IRequestHandler<AddCustomerQuery, Domain.Customers>
    {
        private readonly CoreContext _coreContext;

        public AddCustomerQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<Domain.Customers> Handle(AddCustomerQuery request, CancellationToken cancellationToken)
        {
            var entry = await _coreContext.Set<Domain.Customers>().AddAsync(request.Customer, cancellationToken);

            await _coreContext.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
    }
}
