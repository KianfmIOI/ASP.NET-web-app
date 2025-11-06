using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int ClassSubjectTeacherId { get; set; }
        public ClassSubjectTeacher? ClassSubjectTeacher { get; set; }

        public DateTime Date { get; set; } 

        public bool IsPresent { get; set; }
    }
}
