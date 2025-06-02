using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrgManager_API.DTO;
using OrgManager_API.Interfaces;
using OrgManager_API.Model;

namespace OrgManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeReposatory empRepo;

        public EmployeeController(IEmployeeReposatory empRepo)
        {
            this.empRepo = empRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = empRepo.GetAllWithDepartments(); // Ensure this includes department info
            var employeeDtos = employees.Select(emp => new EmployeeWithDepartmentDto
            {
                Id = emp.Id,
                Name = emp.Name,
                Salary = emp.Salary,
                DeptId = emp.DeptId,
                DepartmentName = emp.Department.Name
            }).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{id:int}", Name = "FindEmployeeById")]
        public IActionResult GetById(int id)
        {
            EmployeeWithDepartmentDto empDto = new EmployeeWithDepartmentDto();
            // Dto or jsonIgnore
            var emp = empRepo.GetEmployeeWithDepartment(id);
            empDto.Id = emp.Id;
            empDto.Name = emp.Name;
            empDto.Salary = emp.Salary;
            empDto.DeptId = emp.DeptId;
            empDto.DepartmentName = emp.Department.Name;
            return Ok(empDto);
        }
        [HttpPost]
        public IActionResult CreateEmployee(Employee Emp)
        {
            empRepo.CreateOne(Emp);
            empRepo.SaveChanges();
            string url = Url.Link("FindDepartmentByID", new { id = Emp.Id });
            return Created(url, Emp);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee newEmployee)
        {
            if (ModelState.IsValid)
            {
                var oldEmp = empRepo.GetById(id);
                oldEmp.Name = newEmployee.Name;
                oldEmp.Salary = newEmployee.Salary;
                oldEmp.DeptId = newEmployee.DeptId;
                empRepo.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, "Employee Updated");
            }
            return BadRequest("Data Not Valid");
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            empRepo.DeleteOne(id);
            empRepo.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent, "Employee Deleted Successfully");
        }
    }
}
