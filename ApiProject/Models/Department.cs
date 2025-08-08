using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

         [Required]
        [StringLength(100)]
        public string? Description { get; set; }

        //navigation property
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
