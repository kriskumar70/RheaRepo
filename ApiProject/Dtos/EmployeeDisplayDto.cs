namespace ApiProject.Dtos
{
    public class EmployeeDisplayDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? DepartmentName { get; set; }
        public string? Bio { get; set; }
        public string[]? Skills { get; set; }
    }
}
