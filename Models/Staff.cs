using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementWebApp.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int StaffRoleId { get; set; }
        public StaffRole? StaffRole { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Social security number")]
        [StringLength(10)]
        public string? SocialSecurity { get; set; } = null!;
    }
}
