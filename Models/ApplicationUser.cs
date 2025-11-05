using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace SchoolManagementWebApp.Models

{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        [StringLength(10)]
        public string SSN { get; set; } = null!;
    }
}
