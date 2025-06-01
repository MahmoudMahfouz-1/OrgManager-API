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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentReposatory deptRepo;
        public DepartmentController(IDepartmentReposatory deptRepo)
        {
            this.deptRepo = deptRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = deptRepo.GetAllWithEmployees();

            var deptDtos = departments.Select(d => new DepartmentWithEmployeeDto
            {
                Id = d.Id,
                Name = d.Name,
                Employees = d.Employees.Select(e => new SimpleEmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToList()
            }).ToList();

            return Ok(deptDtos);
        }

        [HttpGet("{id:int}", Name = "FindDepartmentByID")]
        public IActionResult GetById(int id)
        {
            var dept = deptRepo.GetDepartmentWithEmployee(id);
            if (dept == null) { return BadRequest(); }
            DepartmentWithEmployeeDto deptDto = new DepartmentWithEmployeeDto()
            {
                Id = dept.Id,
                Name = dept.Name,
                ManagerName = dept.ManagerName,
                Employees = dept.Employees.Select(e => new SimpleEmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToList()
            };
            return Ok(deptDto);

        }
        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentCreateDto newDeptDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = new Department
            {
                Name = newDeptDto.Name,
                ManagerName = newDeptDto.ManagerName
            };

            deptRepo.CreateOne(department);
            deptRepo.SaveChanges();

            string url = Url.Link("FindDepartmentByID", new { id = department.Id });
            return Created(url, new { department.Id, department.Name, department.ManagerName });

        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment([FromRoute] int id, [FromBody] DepartmentCreateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldDept = deptRepo.GetById(id);
            if (oldDept == null)
                return NotFound("Department not found");

            oldDept.Name = updateDto.Name;
            oldDept.ManagerName = updateDto.ManagerName;
            deptRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            var dept = deptRepo.GetById(id);
            if (dept == null)
                return NotFound("Department not found");

            deptRepo.DeleteOne(id);
            deptRepo.SaveChanges();

            return NoContent();
        }

    }
}
