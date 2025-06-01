using Microsoft.EntityFrameworkCore;
using OrgManager_API.Interfaces;
using OrgManager_API.Model;

namespace OrgManager_API.Reposatorties
{
    public class DepartmentReposatory : IDepartmentReposatory
    {
        private readonly ApplicationDbContext context;
        public DepartmentReposatory(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Department> GetAll()
        {
            var departmentList = context.Department.ToList();
            return departmentList;
        }
        public Department GetById(int id)
        {
            var department = context.Department.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return null;
            }
            return department;
        }
        public void CreateOne(Department department)
        {
            context.Department.Add(department);
        }

        public void UpdateOne(Department department)
        {
            context.Department.Update(department);
        }

        public void DeleteOne(int id)
        {
            var dept = GetById(id);
            context.Department.Remove(dept);
        }
        public List<Department> GetAllWithEmployees()
        {
            return context.Department.Include(d => d.Employees).ToList();
        }
        public Department GetDepartmentWithEmployee(int id)
        {
            return context.Department.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}