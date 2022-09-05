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
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger _logger; 
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IOptions<LoggerOptions> _loggerOptions;

        public DepartmentController(ILogger<DepartmentController> logger, IOptions<LoggerOptions> loggerOptions, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _loggerOptions = loggerOptions;
        }

        [HttpDelete("department-types/delete")]
        public ActionResult DeleteDepartment(int id)
        {
            _logger.LogInformation("Department delete");
            var log = _loggerOptions.Value.Path;

            _departmentRepository.Delete(id);
            return Ok();
        }

        [HttpPost("department-types/create")]
        public ActionResult<int> CreateDepartment(string description)
        {
            _logger.LogInformation("Department add");
            var log = _loggerOptions.Value.Path;

            return Ok(_departmentRepository.Create(new Department
            {
                Description = description,
            }));
        }

        [HttpGet("department-types/get/all")]
        public ActionResult<IList<DepartmentDto>> GetAllDepartment()
        {
            _logger.LogInformation("department getall");
            var log = _loggerOptions.Value.Path;

            return Ok(_departmentRepository.GetAll().Select(department => new DepartmentDto
            {
                Id = department.Id,
                Description = department.Description,

            }).ToList());
        }

        [HttpGet("get/{id}")]
        public ActionResult<DepartmentDto> GetByIdDepartment([FromRoute] int id)
        {
            _logger.LogInformation("Department get");
            var log = _loggerOptions.Value.Path;

            var department = _departmentRepository.GetById(id);

            return Ok(new DepartmentDto
            {
                Id = department.Id,
                Description = department.Description,
            });
        }
    }
}
