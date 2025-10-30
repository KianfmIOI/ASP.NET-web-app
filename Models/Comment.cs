using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace SchoolManagementWebApp.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string CommentContent { get; set; } = null!;
        /* who's going to write the comment?
        *principal and teacher
        *how to identify if the user's admin or teacher
        *add the identity shit
        *
        */
    }
}
