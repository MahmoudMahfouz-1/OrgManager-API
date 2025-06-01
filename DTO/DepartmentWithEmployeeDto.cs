namespace OrgManager_API.DTO
{
    public class DepartmentWithEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<SimpleEmployeeDto> Employees { get; set; }
    }
    public class SimpleEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
