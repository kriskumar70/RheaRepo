namespace ApiProject.Dtos
{
    public class EmployeeCreateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int DepartmentId { get; set; } 
        public string? Bio { get; set; }
        public List<int> SelectedSkillIds { get; set; } = new List<int>(); 
    }
}
