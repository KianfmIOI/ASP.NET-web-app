using SchoolManagementWebApp.Models.SchoolManagementWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Security.Claims;

namespace SchoolManagementWebApp.Models
{
    
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string? FullName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [Display(Name ="Social security number")]
        [StringLength(10)]
        public string? SocialSecurity { get; set; } = null!;
        [Display(Name = "Field of profession")]
        public string FIeldOfProfession { get; set; } = null!;
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

        public ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; } = new List<ClassSubjectTeacher>();

    }
}
