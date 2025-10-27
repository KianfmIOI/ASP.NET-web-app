using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public SubjectName SubjectName { get; set; }

        public ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; } = new List<ClassSubjectTeacher>();
    }
}
