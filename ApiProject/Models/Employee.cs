using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Employee
    { 
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }
        public int DepartmentId { get; set; }

        //we use navigation properties to load related data using ef core eager loading
        //navigation property
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        //navigation property
        public Profile? Profile { get; set; }

        //navigation property
        public ICollection<Skill> Skills { get; set; } = new List<Skill>(); 
    }
}
