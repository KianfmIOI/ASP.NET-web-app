using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Models;
using SchoolManagementWebApp.Models.SchoolManagementWebApp.Models;
using System.Diagnostics;

namespace SchoolManagementWebApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Principal> Principals{ get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Attendance> Attendances{ get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<ClassSubjectTeacher> ClassSubjectTeachers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Student>()
            .HasIndex(s => s.SocialSecurity)
            .IsUnique();
        builder.Entity<Teacher>()
            .HasIndex(t => t.SocialSecurity)
            .IsUnique();
        base.OnModelCreating(builder);
    }
}
