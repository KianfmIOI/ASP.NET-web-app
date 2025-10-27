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
            public Student Student { get; set; } = null!;

            [Required]
            public int TeacherId { get; set; }
            public Teacher Teacher { get; set; } = null!;

            [Required]
            public int SubjectId { get; set; }
            public Subject Subject { get; set; } = null!;

            [Required]
            public int ClassId { get; set; }
            public Class Class { get; set; } = null!;

            public string? comment { get; set; }
            [Range(0, 20)]
            public float Score { get; set; }

            [DataType(DataType.Date)]
            public DateTime DateAssigned { get; set; } = DateTime.Now;
        }
    }
}
