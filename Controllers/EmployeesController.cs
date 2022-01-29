using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Commands.Employees;
using Northwind.Domain;
using Northwind.Queries.Employees;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly CoreContext _publicContext;
        private readonly IMediator _mediator;
        public EmployeesController(CoreContext publicContext, IMediator mediator)
        {
            _publicContext = publicContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _mediator.Send(new GetAllEmployeesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeQuery
            {
                Id = id
            });

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> PostNew(Employees employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.LastName))
            {
                return BadRequest(new
                {
                    Error = "You must provide proper names"
                });
            }

            var entity = _mediator.Send(new AddEmployeeQuery
            {
                Employee = employee
            });

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]Employees employeeToUpdate)
        {
            var employee = await _publicContext.Employees.FindAsync(id);

            if (employee is null)
            {
                return NotFound();
;           }

            return Ok(_mediator.Send(new UpdateEmployeeQuery
            {
                Id = id,
                Employee = employeeToUpdate
            }));
        }
    }
}
