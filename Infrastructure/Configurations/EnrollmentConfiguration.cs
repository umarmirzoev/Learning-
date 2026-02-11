using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollment");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentId);
        builder.HasOne(x => x.Course) .WithMany(x => x.Enrollments).HasForeignKey(x => x.CourseId);
        builder.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();
    }
}
