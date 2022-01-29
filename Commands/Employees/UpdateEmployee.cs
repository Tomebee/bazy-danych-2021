using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Domain;

namespace Northwind.Commands.Employees
{
    public class UpdateEmployeeQuery : IRequest<Domain.Employees>
    {
        public int Id { get; set; }
        public Domain.Employees Employee { get; set; }
    }

    internal sealed class UpdateEmployeeQueryHandler : IRequestHandler<UpdateEmployeeQuery, Domain.Employees>
    {
        private readonly CoreContext _coreContext;

        public UpdateEmployeeQueryHandler(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<Domain.Employees> Handle(UpdateEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _coreContext.Employees.FindAsync(request.Id);

            employee.Address = request.Employee.Address;
            employee.BirthDate = request.Employee.BirthDate;
            employee.City = request.Employee.City;
            employee.Country = request.Employee.Country;
            employee.Extension = request.Employee.Extension;
            employee.HomePhone = request.Employee.HomePhone;
            employee.Notes = request.Employee.Notes;
            employee.Photo = request.Employee.Photo;
            employee.PostalCode = request.Employee.PostalCode;
            employee.Region = request.Employee.Region;
            employee.Title = request.Employee.Title;

            var entry = _coreContext.Update(employee);
            await _coreContext.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
    }
}
