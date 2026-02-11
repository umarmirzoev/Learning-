using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lesson");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.HasOne(x => x.Module).WithMany(x => x.Lessons).HasForeignKey(x => x.ModuleId).OnDelete(DeleteBehavior.Cascade);
    }
}
