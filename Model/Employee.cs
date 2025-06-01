using System.ComponentModel.DataAnnotations.Schema;

namespace OrgManager_API.Model
{
    public class Employee
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Salary { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}
