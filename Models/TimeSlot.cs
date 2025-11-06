using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Session's number")]
        public int SessionNumber{ get; set; }

        [DataType(DataType.Time)]
        [Display (Name="Starting Time")]
        public TimeOnly StartingTime{ get; set; }

        [DataType(DataType.Time)]
        [Display (Name="Ending Time")]
        public TimeOnly EndingTime{ get; set; }

    }
}
