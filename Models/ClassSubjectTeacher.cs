using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class ClassSubjectTeacher
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }
        public Class? Class{ get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
