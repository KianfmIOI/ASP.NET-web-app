using System.ComponentModel.DataAnnotations;

namespace SchoolManagementWebApp.Models
{
    public class Principal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;
    }
}
