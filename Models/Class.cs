using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace SchoolManagementWebApp.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; } = new List<ClassSubjectTeacher>();

    }
}
