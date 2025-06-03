using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrgManager_API.Model
{
    public class Department
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string ManagerName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
