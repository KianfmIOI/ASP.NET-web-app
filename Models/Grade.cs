using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementWebApp.Models
{

    namespace SchoolManagementWebApp.Models
    {
        public class Grade
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public int StudentId { get; set; }
            public Student? Student { get; set; }

            [Required]
            public int ClassSubjectTeacherId { get; set; }
            public ClassSubjectTeacher? ClassSubjectTeacher { get; set; }

            public string? comment { get; set; }
            [Range(0, 20)]
            public float Score { get; set; }

            [DataType(DataType.Date)]
            public DateTime DateAssigned { get; set; }
        }
    }
}
