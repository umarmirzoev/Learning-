using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseProgressConfiguration : IEntityTypeConfiguration<CourseProgress>
{
    public void Configure(EntityTypeBuilder<CourseProgress> builder)
    {
        builder.ToTable("CourseProgress");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Student).WithMany(x => x.CourseProgresses).HasForeignKey(x => x.StudentId);
        builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId);
        builder.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();
    }
}
