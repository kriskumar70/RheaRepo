using ApiProject.Models;
using ApiProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    //api/departments
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        //ctor: constructor is a method that creates the object of a class for us

        //we want DepartmentRepository class object...we do this using the IDepartmentRepository

        //IoC and DI that we have configured, will look at the ctor parameter and crete the object for us and inject it in the consrtuctor parameter
        //DI: Dependecy Injection

        //global variable/field
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
          _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _departmentRepository.GetDepartments();
            return Ok(departments);
        }

        //api/departments/id
        [HttpGet("{id}")]
        public IActionResult GetDepartments(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();   //404 error
            }
            return Ok(department);
        }

        //post is used for creation
        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            //server-side validation
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                _departmentRepository.Save(); //this method will save the data permanently in the database

                // from a Post action method we always return a status code of 201 which means Created status code
                return Created("", new { Message = "Department added successfully" });
            }
            return BadRequest(new { Error = "Error while creating new deparment" });
        }

        //put is used for updating the data
        [HttpPut("{id}")]
        public IActionResult EditDepartment(int id, Department department)
        {
            if (id != department.Id)
                return BadRequest(new { Error = "Id in the url does not match the Id of the department in the request body" });

            //server-side validation
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(department);
                _departmentRepository.Save(); //this method will save the data permanently in the database

                // from a Post action method we always return a status code of 201 which means Created status code
                return Ok(new { Message = "Department updated successfully" });
            }
            return BadRequest(new { Error = "Error while updating deparment" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            _departmentRepository.Delete(id);
            _departmentRepository.Save();
            return Ok(new { Message = "Department deleted successfully" });
        }
    }
}
