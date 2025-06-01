using System.ComponentModel.DataAnnotations;

namespace OrgManager_API.DTO
{
    public class DepartmentCreateDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string ManagerName { get; set; }
    }
}
