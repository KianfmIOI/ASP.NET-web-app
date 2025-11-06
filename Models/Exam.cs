using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SchoolManagementWebApp.Models
{
    public class Exam
    {
        public int Id { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        [Display(Name ="Date & Time of exam")]
        public DateTime ExamDate{ get; set; }
        [DataType(DataType.Time)]
        [Display(Name ="Ending time of the exam")]
        public TimeOnly EndingTime { get; set; }
        [Display(Name ="Optional details")]
        public string ExamDetails { get; set; } = null!;
    }
}
