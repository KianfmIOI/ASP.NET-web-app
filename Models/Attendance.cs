using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.Today;

        public bool IsPresent { get; set; }
    }
}
