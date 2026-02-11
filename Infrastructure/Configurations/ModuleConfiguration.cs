using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("Module");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.HasOne(x => x.Course).WithMany(x => x.Modules).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
    }
}
