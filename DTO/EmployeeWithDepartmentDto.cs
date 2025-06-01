namespace OrgManager_API.DTO
{
    public class EmployeeWithDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int DeptId { get; set; }
        public string DepartmentName { get; set; }
    }
}
