using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class StaffRole
    {
        [Key]
        public int Id { get; set; }
        public string? RoleName { get; set; } = string.Empty;
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
    }
}
