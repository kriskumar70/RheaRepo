using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Profile
    {
        [Key, ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public string? Bio { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        //navigation property
        public Employee Employee { get; set; }
    }
}
