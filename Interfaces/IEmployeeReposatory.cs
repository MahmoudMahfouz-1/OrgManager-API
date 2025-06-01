using OrgManager_API.Model;

namespace OrgManager_API.Interfaces
{
    public interface IEmployeeReposatory
    {
        public List<Employee> GetAll();
        public Employee GetById(int id);
        public void CreateOne(Employee employee);
        public void UpdateOne(Employee employee);
        public void DeleteOne(int id);
        public Employee GetEmployeeWithDepartment(int id);
        public List<Employee> GetAllWithDepartments();
        public void SaveChanges();

    }
}
