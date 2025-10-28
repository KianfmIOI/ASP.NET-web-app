using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string SubjectName { get; set; } = null!;
        public ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; } = new List<ClassSubjectTeacher>();
    }
}
