using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Models;
using SchoolManagementWebApp.Models.SchoolManagementWebApp.Models;
using System.Diagnostics;
using System.Reflection.Emit;

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
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<StaffRole> StaffRoles { get; set; }

    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Schedule> Schedules { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Student>()
            .HasIndex(s => s.SocialSecurity)
            .IsUnique();
        
        builder.Entity<Teacher>()
            .HasIndex(t => t.SocialSecurity)
            .IsUnique();
        
        builder.Entity<Subject>()
            .HasIndex(t => t.SubjectName)
            .IsUnique();

        builder.Entity<Staff>()
            .HasOne(r => r.StaffRole)
            .WithMany(s => s.Staffs);

        builder.Entity<Exam>()
            .HasIndex(dt => dt.ExamDate)
            .IsUnique();
        builder.Entity<TimeSlot>()
            .HasIndex(st => st.StartingTime)
            .IsUnique();
        builder.Entity<Schedule>()
        .HasIndex(s => new { s.DayOfWeek, s.TimeSlotId, s.ClassSubjectTeacherId })
        .IsUnique();
        base.OnModelCreating(builder);
    }
}
