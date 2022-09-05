using EmployeeService.Dats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Timesheets.Models.Options;
using Timesheets.Models;
using Timesheets.Services.Impl;
using Timesheets.Services;

namespace Timesheets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger; 
        private readonly IEmployeeRepository _employeeRepository; 
        private readonly IOptions<LoggerOptions> _loggerOptions;

        public EmployeeController(ILogger<EmployeeController> logger, IOptions<LoggerOptions> loggerOptions, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _loggerOptions = loggerOptions;
        }

        [HttpDelete("employee/delete")]
        public ActionResult DeleteEmployee(int id)
        {
            _logger.LogInformation("Employee delete");
            var log = _loggerOptions.Value.Path;

            _employeeRepository.Delete(id);
            return Ok();
        }

        [HttpPost("employee/create")]
        public ActionResult<int> CreateEmployee(int _DepartmentId, int _EmployeeTypeId,
            string _FirstName, string _Surname, string _Patronymic, int _Salary)
        {
            _logger.LogInformation("Employee add");
            var log = _loggerOptions.Value.Path;


            return Ok(_employeeRepository.Create(new EmployeeService.Dats.Employee
            {
                DepartmentId = _DepartmentId,
                EmployeeTypeId = _EmployeeTypeId,
                FirstName = _FirstName,
                Surname = _Surname,
                Patronymic = _Patronymic,
                Salary = _Salary
            }));
        }

        [HttpPost("employee/update")]
        public ActionResult<int> UpdateEmployee(int _Id, int _DepartmentId, int _EmployeeTypeId,
            string _FirstName, string _Surname, string _Patronymic, int _Salary)
        {
            _logger.LogInformation("Employee update");
            var log = _loggerOptions.Value.Path;

            var employee = _employeeRepository.GetById(_Id);

            _employeeRepository.Update(new EmployeeService.Dats.Employee
            {
                DepartmentId = _DepartmentId,
                EmployeeTypeId = _EmployeeTypeId,
                FirstName = _FirstName,
                Surname = _Surname,
                Patronymic = _Patronymic,
                Salary = _Salary
            });
            return Ok();
        }

        [HttpGet("employee/get/all")]
        public ActionResult<IList<EmployeeDto>> GetAllEmployee()
        {
            _logger.LogInformation("Employee getall");
            var log = _loggerOptions.Value.Path;

            return Ok(_employeeRepository.GetAll().Select(employee => new EmployeeDto
            {
                Id = employee.Id,
                DepartmentId = employee.DepartmentId,
                EmployeeTypeId = employee.EmployeeTypeId,
                FirstName = employee.FirstName,
                Surname = employee.Surname,
                Patronymic = employee.Patronymic,
                Salary = employee.Salary

            }).ToList());
        }

        [HttpGet("get/{id}")]
        public ActionResult<EmployeeDto> GetByIdEmployee([FromRoute] int id)
        {
            _logger.LogInformation("Employee get");
            var log = _loggerOptions.Value.Path;

            var employee = _employeeRepository.GetById(id);

            return Ok(new EmployeeDto
            {
                Id = employee.Id,
                DepartmentId = employee.DepartmentId,
                EmployeeTypeId = employee.EmployeeTypeId,
                FirstName = employee.FirstName,
                Surname = employee.Surname,
                Patronymic = employee.Patronymic,
                Salary = employee.Salary
            });
        }
    }
}