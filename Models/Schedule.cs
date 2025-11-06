using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Day of the week")]
        public DayOfWeek DayOfWeek{ get; set; }

        public int TimeSlotId { get; set; }
        public TimeSlot? TimeSlot { get; set; }

        public int ClassSubjectTeacherId { get; set; }
        public ClassSubjectTeacher? ClassSubjectTeacher { get; set; }

    }
}
