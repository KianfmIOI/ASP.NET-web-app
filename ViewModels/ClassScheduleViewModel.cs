using SchoolManagementWebApp.Models;

namespace SchoolManagementWebApp.ViewModels
{
    public class ClassScheduleViewModel
    {
        public List<ClassSubjectTeacher>? ClassSubjectTeachers { get; set; }
        public List<Schedule>? Schedules{ get; set; }
    }
}
