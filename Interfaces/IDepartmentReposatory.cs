using OrgManager_API.Model;

namespace OrgManager_API.Interfaces
{
    public interface IDepartmentReposatory
    {
        public List<Department> GetAll();
        public Department GetById(int id);
        public void CreateOne(Department department);
        public void UpdateOne(Department department);
        public void DeleteOne(int id);
        public List<Department> GetAllWithEmployees();
        public Department GetDepartmentWithEmployee(int id);
        public void SaveChanges();
    }
}