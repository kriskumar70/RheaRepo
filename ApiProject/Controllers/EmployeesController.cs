using ApiProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Dtos;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        //injecting IEmployeeRepository in EmployeesController's constructor
        //doing Dependency Injection (DI) for EmployeeRepository
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployeesWithDetails()
        {
            var employees = _employeeRepository.GetAllEmployeesWithDetails();

            var employeesDto = new List<EmployeeDisplayDto>();

            foreach (var employee in employees)
            {
                employeesDto.Add(new EmployeeDisplayDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Profile.Email,
                    DepartmentName = employee.Department.Name,
                    Bio = employee.Profile.Bio,
                    Skills = employee.Skills.Select(x => x.Name).ToArray()
                });
            }

            return Ok(employeesDto);
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeCreateDto employeeCreateDto)
        { 
            if(!ModelState.IsValid)
                return BadRequest(ModelState); //400 error + error message

            try 
            {
                //we cannot insert/update/delete any data
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
    }
}
