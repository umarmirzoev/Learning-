using Npgsql;
using Microsoft.EntityFrameworkCore;
public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
{
    public DbSet<CourseProgress> CourseProgresses  {get; set;}
    public DbSet<Enrollment> Enrollments  {get; set;}
    public DbSet<Lesson> Lessons  {get; set;}
    public DbSet<InstructorProfile> InstructorProfiles{get; set;}
    public DbSet<Module> Modules   {get; set;}
    public DbSet<StudentProfile> StudentProfiles   {get; set;}
    public DbSet<User> Users {get; set;}
        public DbSet<Course> Courses {get; set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Course>()
        .HasOne(c => c.Instructor)
        .WithMany(i => i.Courses)
        .HasForeignKey(c => c.InstructorId)
        .OnDelete(DeleteBehavior.Restrict);

    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>()
        .HasOne(u => u.StudentProfile)
        .WithOne(s => s.User)
        .HasForeignKey<StudentProfile>(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<User>()
        .HasOne(u => u.InstructorProfile)
        .WithOne(i => i.User)
        .HasForeignKey<InstructorProfile>(i => i.UserId)
        .OnDelete(DeleteBehavior.Cascade);


    modelBuilder.Entity<Course>()
        .HasOne(c => c.Instructor)
        .WithMany(i => i.Courses)
        .HasForeignKey(c => c.InstructorId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Module>()
        .HasOne(m => m.Course)
        .WithMany(c => c.Modules)
        .HasForeignKey(m => m.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Lesson>()
        .HasOne(l => l.Module)
        .WithMany(m => m.Lessons)
        .HasForeignKey(l => l.ModuleId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Course)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<CourseProgress>()
        .HasOne(cp => cp.Student)
        .WithMany(s => s.CourseProgresses)
        .HasForeignKey(cp => cp.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<CourseProgress>()
        .HasOne(cp => cp.Course)
        .WithMany()
        .HasForeignKey(cp => cp.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

}
}