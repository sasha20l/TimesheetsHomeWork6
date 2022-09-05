using EmployeeService.Dats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Timesheets.Models.Options;
using Timesheets.Models;
using Timesheets.Services;

namespace Timesheets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly ILogger _logger; 
        private readonly IEmployeeTypeRepository _employeeTypeRepository; 
        private readonly IOptions<LoggerOptions> _loggerOptions;

        public EmployeeTypeController(ILogger<EmployeeTypeController> logger, IOptions<LoggerOptions> loggerOptions, IEmployeeTypeRepository employeeTypeRepository)
        {
            _logger = logger;
            _employeeTypeRepository = employeeTypeRepository;
            _loggerOptions = loggerOptions;
        }

        [HttpDelete("employee-types/delete")]
        public ActionResult DeleteEmployeeType(int id)
        {
            _logger.LogInformation("EmployeeType delete");
            var log = _loggerOptions.Value.Path;

            _employeeTypeRepository.Delete(id);
            return Ok();
        }

        [HttpPost("employee-types/create")]
        public ActionResult<int> CreateEmployeeType(string description)
        {
            _logger.LogInformation("EmployeeType add");
            var log = _loggerOptions.Value.Path;

            return Ok(_employeeTypeRepository.Create(new EmployeeType
            {
                Description = description,
            }));
        }

        [HttpGet("employee-types/get/all")]
        public ActionResult<IList<EmployeeTypeDto>> GetAllEmployeeType()
        {
            _logger.LogInformation("EmployeeType getall");
            var log = _loggerOptions.Value.Path;

            return Ok(_employeeTypeRepository.GetAll().Select(employeeType => new EmployeeTypeDto
            {
                Id = employeeType.Id,
                Description = employeeType.Description,

            }).ToList());
        }

        [HttpGet("get/{id}")]
        public ActionResult<EmployeeTypeDto> GetByIdEmployeeType([FromRoute] int id)
        {
            _logger.LogInformation("EmployeeType get");
            var log = _loggerOptions.Value.Path;

            var employee = _employeeTypeRepository.GetById(id);

            return Ok(new EmployeeTypeDto
            {
                Id = employee.Id,
                Description = employee.Description,
            });
        }
    }
}
