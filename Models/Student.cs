using SchoolManagementWebApp.Models.SchoolManagementWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementWebApp.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Father's name")]
        public string? FatherName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Social security number")]

        public string? SocialSecurity { get; set; } = string.Empty;
        public int? ClassId { get; set; }
        public Class? Class { get; set; }
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

    }
}
