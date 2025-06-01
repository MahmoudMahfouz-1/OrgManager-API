using Microsoft.EntityFrameworkCore;
using OrgManager_API.Interfaces;
using OrgManager_API.Model;

namespace OrgManager_API.Reposatories
{
    public class EmployeeReposatory : IEmployeeReposatory
    {
        private readonly ApplicationDbContext context;

        public EmployeeReposatory(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void CreateOne(Employee employee)
        {
            context.Employee.Add(employee);
        }

        public void DeleteOne(int id)
        {
            var emp = GetById(id);
            context.Employee.Remove(emp);
        }

        public List<Employee> GetAll()
        {
            var employeeList = context.Employee.ToList();
            return employeeList;
        }

        public Employee GetById(int id)
        {
            var employee = context.Employee.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void UpdateOne(Employee employee)
        {
            context.Employee.Update(employee);
        }

        public Employee GetEmployeeWithDepartment(int id)
        {
            var employee = context.Employee.Include(x => x.Department).FirstOrDefault(e => e.Id == id);
            return employee;
        }
        public List<Employee> GetAllWithDepartments()
        {
            var employees = context.Employee.Include(e => e.Department).ToList();
            return employees;
        }

    }
}
